using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IEventoRepository : IGenericRepository<Evento>
{
    Task<(IReadOnlyList<Evento> Items, int TotalCount)> BuscarPaginadoAsync(
        string? categoria, EventoStatus? status, int page, int pageSize, CancellationToken ct = default);
}
