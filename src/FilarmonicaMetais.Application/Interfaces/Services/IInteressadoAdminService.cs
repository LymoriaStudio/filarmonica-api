using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IInteressadoAdminService
{
    Task<IReadOnlyList<InteressadoAdminDto>> GetAllAsync(CancellationToken ct = default);
    Task<InteressadoAdminDto> UpdateStatusAsync(Guid id, UpdateInteressadoStatusRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
