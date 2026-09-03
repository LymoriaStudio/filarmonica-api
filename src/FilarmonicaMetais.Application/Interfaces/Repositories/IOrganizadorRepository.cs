using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IOrganizadorRepository : IGenericRepository<Organizador>
{
    Task<IReadOnlyList<Organizador>> GetOrdenadosAsync(CancellationToken ct = default);
}
