using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class InstrumentoRepository : GenericRepository<Instrumento>, IInstrumentoRepository
{
    public InstrumentoRepository(AppDbContext context) : base(context) { }

    // AsSplitQuery em todo Include(Galeria): com LEFT JOIN normal, um instrumento
    // sem nenhuma foto ainda gera uma linha só de NULLs pro lado de
    // instrumento_fotos — no provider MySQL (Pomelo) isso materializa uma
    // InstrumentoFoto "fantasma" rastreada pelo EF, que o SaveChanges tenta
    // fazer UPDATE (linha que não existe) e aborta com
    // DbUpdateConcurrencyException antes do INSERT da foto real acontecer.
    // Split query busca a galeria numa consulta separada (WHERE InstrumentoId
    // IN (...)), sem esse problema.

    // Sobrescreve o genérico: no Supabase a galeria vinha embutida no mesmo array
    // da linha (gallery text[]), então a listagem pública sempre trazia as fotos junto.
    public new async Task<IReadOnlyList<Instrumento>> GetAllAsync(CancellationToken ct = default) =>
        await DbSet.AsNoTracking().Include(i => i.Galeria.OrderBy(f => f.Ordem)).AsSplitQuery().ToListAsync(ct);

    public Task<Instrumento?> GetBySlugAsync(string slug, CancellationToken ct = default) =>
        DbSet.AsNoTracking()
            .Include(i => i.Galeria.OrderBy(f => f.Ordem))
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Slug.ToLower() == slug.ToLower(), ct);

    public Task<Instrumento?> GetByIdComGaleriaAsync(Guid id, CancellationToken ct = default) =>
        DbSet.Include(i => i.Galeria.OrderBy(f => f.Ordem))
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == id, ct);
}
