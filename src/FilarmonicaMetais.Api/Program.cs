using System.Text;
using FilarmonicaMetais.Api.Extensions;
using FilarmonicaMetais.Api.Middlewares;
using FilarmonicaMetais.Application;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;
using FilarmonicaMetais.Infrastructure;
using FilarmonicaMetais.Infrastructure.Auth;
using FilarmonicaMetais.Infrastructure.FileStorage;
using FilarmonicaMetais.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Filarmônica de Metais API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe apenas o token JWT (sem o prefixo 'Bearer ').",
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
    ?? throw new InvalidOperationException("Seção 'Jwt' não configurada.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1),
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole(nameof(UserRole.Admin)));

var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Alinhado ao limite de upload do front (com folga) para fotos/PDFs.
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 20 * 1024 * 1024;
});

var app = builder.Build();

await ApplyMigrationsAndSeedAdminAsync(app);

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Swagger fica exposto também fora de Development de propósito: este ambiente
// ainda é de teste (Railway). Fechar antes de apontar dados reais de produção.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Serve os arquivos enviados via IFileStorageService (fotos, PDFs) como estático,
// no mesmo caminho que LocalFileStorageService.ResolvePublicUrl monta as URLs
// (FileStorage:PublicBaseUrl + "/" + caminho relativo). Sem isso, ResolvePublicUrl
// devolvia uma URL que nada respondia.
var fileStorageOptions = app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<LocalFileStorageOptions>>().Value;
var uploadsPath = Path.GetFullPath(fileStorageOptions.BasePath);
Directory.CreateDirectory(uploadsPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/media",
});

app.UseCors("Default");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

// Aplica migrations pendentes e cria o primeiro usuário Admin se nenhum existir ainda —
// sem isso, um banco novo fica sem porta de entrada (só Admin pode criar usuários).
// Só age se Seed:AdminEmail/AdminPassword estiverem configurados; nunca sobrescreve um admin existente.
// Falha aqui não derruba a API (útil em dev, quando o banco pode não estar disponível ainda).
static async Task ApplyMigrationsAndSeedAdminAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        var adminEmail = app.Configuration["Seed:AdminEmail"];
        var adminPassword = app.Configuration["Seed:AdminPassword"];

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            logger.LogInformation("Seed:AdminEmail/AdminPassword não configurados — nenhum usuário admin será criado automaticamente.");
            return;
        }

        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        if (await uow.Usuarios.GetByEmailAsync(adminEmail) is not null)
            return;

        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        await uow.Usuarios.AddAsync(new Usuario
        {
            FullName = app.Configuration["Seed:AdminFullName"] ?? "Administrador",
            Email = adminEmail,
            PasswordHash = hasher.Hash(adminPassword),
            Role = UserRole.Admin,
            IsActive = true,
        });
        await uow.SaveChangesAsync();

        logger.LogInformation("Usuário admin inicial criado: {Email}", adminEmail);
    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "Não foi possível aplicar migrations/seed automático na inicialização.");
    }
}

// Necessário para o WebApplicationFactory de testes de integração enxergar o Program
// como classe pública parcial — inofensivo aqui, mas é convenção do template minimal hosting.
public partial class Program { }
