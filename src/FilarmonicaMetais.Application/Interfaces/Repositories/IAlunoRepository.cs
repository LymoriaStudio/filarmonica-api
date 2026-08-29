using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IAlunoRepository : IGenericRepository<Aluno>
{
    // Busca por nome/e-mail case-insensitive explícita nos dois lados (regra #7 do plano) —
    // .Contains() do EF vira LIKE sensível a maiúsculas no Postgres, insensível no SQL Server.
    Task<(IReadOnlyList<Aluno> Items, int TotalCount)> BuscarPaginadoAsync(
        string? busca, AlunoStatus? status, int page, int pageSize, CancellationToken ct = default);
}
