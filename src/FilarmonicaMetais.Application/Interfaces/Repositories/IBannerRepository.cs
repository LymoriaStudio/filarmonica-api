using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IBannerRepository : IGenericRepository<Banner>
{
    Task<IReadOnlyList<Banner>> GetAtivosOrdenadosAsync(CancellationToken ct = default);
}
