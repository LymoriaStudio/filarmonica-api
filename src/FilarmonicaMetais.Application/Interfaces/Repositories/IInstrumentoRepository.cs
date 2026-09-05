using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IInstrumentoRepository : IGenericRepository<Instrumento>
{
    // Redeclarado aqui (não só com `new` na classe concreta) — sem isso, quem
    // chama via IInstrumentoRepository (todo mundo, através de IUnitOfWork)
    // cai no GetAllAsync genérico da base, sem Include(Galeria) nenhum, porque
    // `new` só esconde o método pra quem usa o tipo concreto diretamente.
    new Task<IReadOnlyList<Instrumento>> GetAllAsync(CancellationToken ct = default);

    Task<Instrumento?> GetBySlugAsync(string slug, CancellationToken ct = default);

    // Sobrescreve a leitura genérica incluindo a galeria (Include) —
    // instrumento sem fotos carregadas não serve para tela nenhuma.
    Task<Instrumento?> GetByIdComGaleriaAsync(Guid id, CancellationToken ct = default);
}
