using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class EventoRepository : GenericRepository<Evento>, IEventoRepository
{
    public EventoRepository(AppDbContext context) : base(context) { }

    public async Task<(IReadOnlyList<Evento> Items, int TotalCount)> BuscarPaginadoAsync(
        string? categoria, EventoStatus? status, int page, int pageSize, CancellationToken ct = default)
    {
        var query = DbSet.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoria))
            query = query.Where(e => e.Categoria.ToLower() == categoria.ToLower());

        if (status.HasValue)
            query = query.Where(e => e.Status == status.Value);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(e => e.Data)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }
}
