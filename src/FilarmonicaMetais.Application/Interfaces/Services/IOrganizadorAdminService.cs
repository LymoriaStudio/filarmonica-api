using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IOrganizadorAdminService
{
    Task<IReadOnlyList<AdminOrganizadorDto>> GetAllAsync(CancellationToken ct = default);
    Task<AdminOrganizadorDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AdminOrganizadorDto> CreateAsync(CreateOrganizadorRequest request, CancellationToken ct = default);
    Task<AdminOrganizadorDto> UpdateAsync(Guid id, UpdateOrganizadorRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
