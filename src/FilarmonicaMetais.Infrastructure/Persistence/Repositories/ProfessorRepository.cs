using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class ProfessorRepository : GenericRepository<Professor>, IProfessorRepository
{
    public ProfessorRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Professor>> GetOrdenadosAsync(CancellationToken ct = default) =>
        await DbSet.AsNoTracking().OrderBy(p => p.DisplayOrder).ToListAsync(ct);
}
