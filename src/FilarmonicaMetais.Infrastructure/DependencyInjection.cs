using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Infrastructure.Auth;
using FilarmonicaMetais.Infrastructure.FileStorage;
using FilarmonicaMetais.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilarmonicaMetais.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["Database:Provider"] ?? "Postgres";
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");

        services.AddDbContext<AppDbContext>(options =>
        {
            switch (provider.Trim().ToLowerInvariant())
            {
                case "sqlserver":
                    options.UseSqlServer(connectionString, sql => sql
                        .MigrationsAssembly("FilarmonicaMetais.Migrations.SqlServer"));
                    break;
                case "mysql":
                    // ServerVersion.AutoDetect abre uma conexão só pra detectar a versão do
                    // servidor — evita fixar "8.0.x" no código quando o alvo real é o
                    // MySQL 9.7.x do cliente (Pomelo entende variações menores da 8.x/9.x
                    // sem trocar de dialect, mas o auto-detect deixa isso automático).
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysql => mysql
                        .MigrationsAssembly("FilarmonicaMetais.Migrations.MySql"));
                    break;
                case "postgres":
                default:
                    options.UseNpgsql(connectionString, npg => npg
                        .MigrationsAssembly("FilarmonicaMetais.Migrations.Postgres"));
                    break;
            }
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.Configure<LocalFileStorageOptions>(configuration.GetSection(LocalFileStorageOptions.SectionName));

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

        return services;
    }
}
