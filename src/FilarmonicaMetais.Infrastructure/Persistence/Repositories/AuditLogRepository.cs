using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class AuditLogRepository : GenericRepository<AuditLog>, IAuditLogRepository
{
    public AuditLogRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<AuditLog>> GetRecentesAsync(int limit, CancellationToken ct = default) =>
        await DbSet.AsNoTracking()
            .OrderByDescending(a => a.DateTime)
            .Take(limit)
            .ToListAsync(ct);
}
