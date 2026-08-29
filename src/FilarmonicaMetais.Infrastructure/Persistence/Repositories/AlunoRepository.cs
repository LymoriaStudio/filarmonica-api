using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class AlunoRepository : GenericRepository<Aluno>, IAlunoRepository
{
    public AlunoRepository(AppDbContext context) : base(context) { }

    public async Task<(IReadOnlyList<Aluno> Items, int TotalCount)> BuscarPaginadoAsync(
        string? busca, AlunoStatus? status, int page, int pageSize, CancellationToken ct = default)
    {
        var query = DbSet.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            var termo = busca.ToLower();
            query = query.Where(a => a.Nome.ToLower().Contains(termo) || a.Email.ToLower().Contains(termo));
        }

        if (status.HasValue)
            query = query.Where(a => a.Status == status.Value);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderBy(a => a.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }
}
