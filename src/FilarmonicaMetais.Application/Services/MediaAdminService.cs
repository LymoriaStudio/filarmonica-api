using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class MediaAdminService : IMediaAdminService
{
    private readonly IUnitOfWork _uow;
    private readonly IFileStorageService _storage;

    public MediaAdminService(IUnitOfWork uow, IFileStorageService storage)
    {
        _uow = uow;
        _storage = storage;
    }

    public async Task<MediaAssetDto> UploadAsync(Stream conteudo, string nomeOriginal, string contentType, string pasta, CancellationToken ct = default)
    {
        var arquivo = await _storage.SaveAsync(conteudo, nomeOriginal, pasta, ct);

        var asset = new MediaAsset
        {
            NomeArquivo = Path.GetFileName(arquivo.CaminhoRelativo),
            NomeOriginal = nomeOriginal,
            CaminhoRelativo = arquivo.CaminhoRelativo,
            Pasta = pasta,
            ContentType = string.IsNullOrEmpty(contentType) ? arquivo.ContentType : contentType,
            TamanhoBytes = arquivo.TamanhoBytes,
        };

        await _uow.MediaAssets.AddAsync(asset, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(asset);
    }

    public async Task<IReadOnlyList<MediaAssetDto>> GetAllAsync(CancellationToken ct = default)
    {
        var assets = await _uow.MediaAssets.GetAllAsync(ct);
        return assets.OrderByDescending(a => a.CreatedAt).Select(ToDto).ToList();
    }

    // Substitui checkMediaUsage() do storageService.ts, que comparava a URL como string
    // contra 4 tabelas. Aqui compara o caminho relativo contra as mesmas colunas de imagem —
    // mais os instrumentos, que o front original não cobria.
    public async Task<MediaUsoDto> CheckUsoAsync(Guid id, CancellationToken ct = default)
    {
        var asset = await _uow.MediaAssets.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(MediaAsset), id);
        var caminho = asset.CaminhoRelativo;
        var usadoPor = new List<string>();

        foreach (var evento in await _uow.Eventos.GetAllAsync(ct))
            if (evento.ImagemCapa == caminho) usadoPor.Add($"Evento: {evento.Titulo}");

        foreach (var professor in await _uow.Professores.GetAllAsync(ct))
            if (professor.Foto == caminho) usadoPor.Add($"Professor: {professor.Nome}");

        foreach (var aluno in await _uow.Alunos.GetAllAsync(ct))
            if (aluno.Foto == caminho) usadoPor.Add($"Aluno: {aluno.Nome}");

        foreach (var curso in await _uow.Cursos.GetAllAsync(ct))
            if (curso.Imagem == caminho) usadoPor.Add($"Curso: {curso.Nome}");

        foreach (var banner in await _uow.Banners.GetAllAsync(ct))
        {
            if (banner.ImageDesktop == caminho || banner.ImageMobile == caminho)
                usadoPor.Add($"Banner: {banner.Title}");
        }

        foreach (var instrumento in await _uow.Instrumentos.GetAllAsync(ct))
        {
            if (instrumento.Imagem == caminho) usadoPor.Add($"Instrumento: {instrumento.Nome}");
            if (instrumento.Galeria.Any(f => f.Url == caminho)) usadoPor.Add($"Instrumento (galeria): {instrumento.Nome}");
        }

        return new MediaUsoDto { EmUso = usadoPor.Count > 0, UsadoPor = usadoPor };
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var asset = await _uow.MediaAssets.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(MediaAsset), id);

        var uso = await CheckUsoAsync(id, ct);
        if (uso.EmUso)
            throw new ConflictException($"Arquivo em uso por: {string.Join(", ", uso.UsadoPor)}. Remova as referências antes de excluir.");

        await _storage.DeleteAsync(asset.CaminhoRelativo, ct);
        _uow.MediaAssets.Remove(asset);
        await _uow.SaveChangesAsync(ct);
    }

    private MediaAssetDto ToDto(MediaAsset a) => new()
    {
        Id = a.Id,
        NomeArquivo = a.NomeArquivo,
        NomeOriginal = a.NomeOriginal,
        CaminhoRelativo = a.CaminhoRelativo,
        Url = _storage.ResolvePublicUrl(a.CaminhoRelativo),
        Pasta = a.Pasta,
        ContentType = a.ContentType,
        TamanhoBytes = a.TamanhoBytes,
        CreatedAt = a.CreatedAt,
    };
}
