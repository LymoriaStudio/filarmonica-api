using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class InstrumentoReadService : IInstrumentoReadService
{
    private readonly IUnitOfWork _uow;
    private readonly IFileStorageService _storage;

    public InstrumentoReadService(IUnitOfWork uow, IFileStorageService storage)
    {
        _uow = uow;
        _storage = storage;
    }

    public async Task<IReadOnlyList<InstrumentoDto>> GetAllAsync(CancellationToken ct = default)
    {
        var instrumentos = await _uow.Instrumentos.GetAllAsync(ct);
        return instrumentos.Select(ToDto).ToList();
    }

    public async Task<InstrumentoDto?> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
        var instrumento = await _uow.Instrumentos.GetBySlugAsync(slug, ct);
        return instrumento is null ? null : ToDto(instrumento);
    }

    private InstrumentoDto ToDto(Instrumento i) => new()
    {
        Id = i.Id,
        Slug = i.Slug,
        Nome = i.Nome,
        Descricao = i.Descricao,
        DescricaoLonga = i.DescricaoLonga,
        ImagemUrl = _storage.ResolvePublicUrl(i.Imagem),
        VideoUrl = i.VideoUrl,
        Cor = i.Cor,
        GaleriaUrls = i.Galeria.OrderBy(f => f.Ordem).Select(f => _storage.ResolvePublicUrl(f.Url)).ToList(),
    };
}
