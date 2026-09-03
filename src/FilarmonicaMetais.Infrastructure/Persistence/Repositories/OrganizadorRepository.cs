using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class OrganizadorRepository : GenericRepository<Organizador>, IOrganizadorRepository
{
    public OrganizadorRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Organizador>> GetOrdenadosAsync(CancellationToken ct = default) =>
        await DbSet.AsNoTracking().OrderBy(o => o.Nome).ToListAsync(ct);
}
