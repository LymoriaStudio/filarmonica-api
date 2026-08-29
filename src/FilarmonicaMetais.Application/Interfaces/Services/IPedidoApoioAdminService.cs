using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IPedidoApoioAdminService
{
    Task<IReadOnlyList<PedidoApoioAdminDto>> GetAllAsync(CancellationToken ct = default);
    Task<PedidoApoioAdminDto> UpdateStatusAsync(Guid id, UpdatePedidoApoioStatusRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
