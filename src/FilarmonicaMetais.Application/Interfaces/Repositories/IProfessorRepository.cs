using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IProfessorRepository : IGenericRepository<Professor>
{
    Task<IReadOnlyList<Professor>> GetOrdenadosAsync(CancellationToken ct = default);
}
