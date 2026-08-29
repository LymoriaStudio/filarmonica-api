using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context) { }

    // .ToLower() explícito dos dois lados (regra #7) — .Contains()/== do EF vira
    // LIKE case-sensitive no Postgres e case-insensitive no SQL Server por padrão;
    // sem a normalização manual, login por e-mail mudaria de comportamento na troca de banco.
    public Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        DbSet.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), ct);

    public Task<Usuario?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default) =>
        DbSet.FirstOrDefaultAsync(u =>
            u.RefreshToken == refreshToken && u.RefreshTokenExpiresAt > DateTime.UtcNow, ct);
}
