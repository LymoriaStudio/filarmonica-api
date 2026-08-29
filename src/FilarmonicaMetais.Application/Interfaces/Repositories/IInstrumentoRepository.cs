using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IInstrumentoRepository : IGenericRepository<Instrumento>
{
    Task<Instrumento?> GetBySlugAsync(string slug, CancellationToken ct = default);

    // Sobrescreve a leitura genérica incluindo a galeria (Include) —
    // instrumento sem fotos carregadas não serve para tela nenhuma.
    Task<Instrumento?> GetByIdComGaleriaAsync(Guid id, CancellationToken ct = default);
}
