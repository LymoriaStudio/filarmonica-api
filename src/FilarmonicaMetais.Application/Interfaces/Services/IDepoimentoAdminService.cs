using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IDepoimentoAdminService
{
    Task<IReadOnlyList<AdminDepoimentoDto>> GetAllAsync(CancellationToken ct = default);
    Task<AdminDepoimentoDto> CreateAsync(CreateDepoimentoRequest request, CancellationToken ct = default);
    Task<AdminDepoimentoDto> UpdateAsync(Guid id, UpdateDepoimentoRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
