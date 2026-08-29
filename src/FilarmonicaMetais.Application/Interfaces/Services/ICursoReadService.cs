using FilarmonicaMetais.Application.DTOs.Site;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface ICursoReadService
{
    Task<IReadOnlyList<CursoDto>> GetAllAsync(CancellationToken ct = default);
}
