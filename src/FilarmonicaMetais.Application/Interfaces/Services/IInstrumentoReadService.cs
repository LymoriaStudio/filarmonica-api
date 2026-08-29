using FilarmonicaMetais.Application.DTOs.Site;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IInstrumentoReadService
{
    Task<IReadOnlyList<InstrumentoDto>> GetAllAsync(CancellationToken ct = default);
    Task<InstrumentoDto?> GetBySlugAsync(string slug, CancellationToken ct = default);
}
