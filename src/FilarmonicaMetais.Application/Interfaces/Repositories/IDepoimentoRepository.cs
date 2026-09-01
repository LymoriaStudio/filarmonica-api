using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IDepoimentoRepository : IGenericRepository<Depoimento>
{
    // Sem filtro de status — usado pelo painel admin, que precisa ver os inativos também.
    Task<IReadOnlyList<Depoimento>> GetOrdenadosAsync(CancellationToken ct = default);

    // Só os ativos — usado pela leitura pública (carrossel do site institucional).
    Task<IReadOnlyList<Depoimento>> GetAtivosOrdenadosAsync(CancellationToken ct = default);
}
