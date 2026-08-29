using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class CursoRepository : GenericRepository<Curso>, ICursoRepository
{
    public CursoRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Curso>> GetAllComProfessorAsync(CancellationToken ct = default) =>
        await DbSet.AsNoTracking().Include(c => c.Professor).ToListAsync(ct);

    public Task<Curso?> GetByIdComProfessorAsync(Guid id, CancellationToken ct = default) =>
        DbSet.Include(c => c.Professor).FirstOrDefaultAsync(c => c.Id == id, ct);
}
