using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class BannerRepository : GenericRepository<Banner>, IBannerRepository
{
    public BannerRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Banner>> GetAtivosOrdenadosAsync(CancellationToken ct = default) =>
        await DbSet.AsNoTracking()
            .Where(b => b.Status == BannerStatus.Ativo)
            .OrderBy(b => b.DisplayOrder)
            .ToListAsync(ct);
}
