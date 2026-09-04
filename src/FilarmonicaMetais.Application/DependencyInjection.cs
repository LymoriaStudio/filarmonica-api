using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FilarmonicaMetais.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IBannerReadService, BannerReadService>();
        services.AddScoped<IInstrumentoReadService, InstrumentoReadService>();
        services.AddScoped<IEventoReadService, EventoReadService>();
        services.AddScoped<IProfessorReadService, ProfessorReadService>();
        services.AddScoped<ICursoReadService, CursoReadService>();
        services.AddScoped<IDepoimentoReadService, DepoimentoReadService>();

        services.AddScoped<IInteressadoService, InteressadoService>();
        services.AddScoped<IPedidoApoioService, PedidoApoioService>();

        services.AddScoped<IBannerAdminService, BannerAdminService>();
        services.AddScoped<IEventoAdminService, EventoAdminService>();
        services.AddScoped<IInstrumentoAdminService, InstrumentoAdminService>();
        services.AddScoped<IProfessorAdminService, ProfessorAdminService>();
        services.AddScoped<ICursoAdminService, CursoAdminService>();
        services.AddScoped<IDepoimentoAdminService, DepoimentoAdminService>();
        services.AddScoped<IAlunoAdminService, AlunoAdminService>();
        services.AddScoped<IOrganizadorAdminService, OrganizadorAdminService>();
        services.AddScoped<IInteressadoAdminService, InteressadoAdminService>();
        services.AddScoped<IPedidoApoioAdminService, PedidoApoioAdminService>();
        services.AddScoped<IDoacaoAdminService, DoacaoAdminService>();
        services.AddScoped<IUsuarioAdminService, UsuarioAdminService>();
        services.AddScoped<IMediaAdminService, MediaAdminService>();
        services.AddScoped<IAuditLogService, AuditLogService>();

        return services;
    }
}
