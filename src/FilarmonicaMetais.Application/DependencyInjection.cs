using Microsoft.Extensions.DependencyInjection;

namespace FilarmonicaMetais.Application;

public static class DependencyInjection
{
    // Hoje sem serviços de aplicação registrados aqui — os *Service concretos
    // ainda serão adicionados por caso de uso (AuthService, AlunoService, etc.)
    // nas próximas fases. O método já existe para a Api chamar builder.Services.AddApplication()
    // desde já, no padrão do projeto de referência.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
