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

        return services;
    }
}
