using FilarmonicaMetais.Application.DTOs.Formularios;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IInteressadoService
{
    Task<Guid> CriarAsync(CreateInteressadoRequest request, CancellationToken ct = default);
}
