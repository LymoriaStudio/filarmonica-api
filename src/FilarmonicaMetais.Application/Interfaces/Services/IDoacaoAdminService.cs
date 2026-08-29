using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IDoacaoAdminService
{
    Task<IReadOnlyList<DoacaoAdminDto>> GetAllAsync(CancellationToken ct = default);
    Task<DoacaoAdminDto> CreateAsync(CreateDoacaoRequest request, CancellationToken ct = default);
    Task<DoacaoAdminDto> UpdateAsync(Guid id, UpdateDoacaoRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
