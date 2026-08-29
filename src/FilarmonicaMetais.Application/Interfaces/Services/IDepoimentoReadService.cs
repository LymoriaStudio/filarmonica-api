using FilarmonicaMetais.Application.DTOs.Site;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IDepoimentoReadService
{
    Task<IReadOnlyList<DepoimentoDto>> GetAllAsync(CancellationToken ct = default);
}
