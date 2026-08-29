using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.DTOs.Site;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IDepoimentoAdminService
{
    Task<DepoimentoDto> CreateAsync(CreateDepoimentoRequest request, CancellationToken ct = default);
    Task<DepoimentoDto> UpdateAsync(Guid id, UpdateDepoimentoRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
