using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IDepoimentoRepository : IGenericRepository<Depoimento>
{
    Task<IReadOnlyList<Depoimento>> GetOrdenadosAsync(CancellationToken ct = default);
}
