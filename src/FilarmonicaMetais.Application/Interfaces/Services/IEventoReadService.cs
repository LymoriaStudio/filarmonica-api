using FilarmonicaMetais.Application.DTOs.Common;
using FilarmonicaMetais.Application.DTOs.Site;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IEventoReadService
{
    Task<PagedResult<EventoDto>> BuscarPaginadoAsync(
        string? categoria, string? status, int page, int pageSize, CancellationToken ct = default);
    Task<EventoDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
}
