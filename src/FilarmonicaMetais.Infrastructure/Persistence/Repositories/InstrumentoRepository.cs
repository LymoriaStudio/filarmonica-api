using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class InstrumentoRepository : GenericRepository<Instrumento>, IInstrumentoRepository
{
    public InstrumentoRepository(AppDbContext context) : base(context) { }

    // Sobrescreve o genérico: no Supabase a galeria vinha embutida no mesmo array
    // da linha (gallery text[]), então a listagem pública sempre trazia as fotos junto.
    public new async Task<IReadOnlyList<Instrumento>> GetAllAsync(CancellationToken ct = default) =>
        await DbSet.AsNoTracking().Include(i => i.Galeria.OrderBy(f => f.Ordem)).ToListAsync(ct);

    public Task<Instrumento?> GetBySlugAsync(string slug, CancellationToken ct = default) =>
        DbSet.AsNoTracking()
            .Include(i => i.Galeria.OrderBy(f => f.Ordem))
            .FirstOrDefaultAsync(i => i.Slug.ToLower() == slug.ToLower(), ct);

    public Task<Instrumento?> GetByIdComGaleriaAsync(Guid id, CancellationToken ct = default) =>
        DbSet.Include(i => i.Galeria.OrderBy(f => f.Ordem))
            .FirstOrDefaultAsync(i => i.Id == id, ct);
}
