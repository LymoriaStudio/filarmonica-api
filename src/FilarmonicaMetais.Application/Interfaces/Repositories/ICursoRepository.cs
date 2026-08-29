using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface ICursoRepository : IGenericRepository<Curso>
{
    // Sempre inclui o Professor (Include) — a FK consolidada depende disso
    // para compor o nome do responsável na leitura.
    Task<IReadOnlyList<Curso>> GetAllComProfessorAsync(CancellationToken ct = default);
    Task<Curso?> GetByIdComProfessorAsync(Guid id, CancellationToken ct = default);
}
