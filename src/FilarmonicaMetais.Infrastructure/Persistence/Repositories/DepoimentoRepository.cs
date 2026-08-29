using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class DepoimentoRepository : GenericRepository<Depoimento>, IDepoimentoRepository
{
    public DepoimentoRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Depoimento>> GetOrdenadosAsync(CancellationToken ct = default) =>
        await DbSet.AsNoTracking().OrderBy(d => d.DisplayOrder).ToListAsync(ct);
}
