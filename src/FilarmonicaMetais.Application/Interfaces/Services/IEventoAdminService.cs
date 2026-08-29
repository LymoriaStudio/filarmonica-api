using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IEventoAdminService
{
    Task<IReadOnlyList<AdminEventoDto>> GetAllAsync(CancellationToken ct = default);
    Task<AdminEventoDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AdminEventoDto> CreateAsync(CreateEventoRequest request, CancellationToken ct = default);
    Task<AdminEventoDto> UpdateAsync(Guid id, UpdateEventoRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
