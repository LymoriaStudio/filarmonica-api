using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace FilarmonicaMetais.Infrastructure.FileStorage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly LocalFileStorageOptions _options;

    public LocalFileStorageService(IOptions<LocalFileStorageOptions> options)
    {
        _options = options.Value;
    }

    public async Task<ArquivoArmazenado> SaveAsync(Stream conteudo, string nomeOriginal, string pasta, CancellationToken ct = default)
    {
        var extensao = Path.GetExtension(nomeOriginal);
        var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
        var caminhoRelativo = Path.Combine(pasta, nomeArquivo).Replace('\\', '/');
        var caminhoFisico = Path.Combine(_options.BasePath, caminhoRelativo);

        Directory.CreateDirectory(Path.GetDirectoryName(caminhoFisico)!);

        await using (var destino = File.Create(caminhoFisico))
        {
            await conteudo.CopyToAsync(destino, ct);
        }

        return new ArquivoArmazenado
        {
            CaminhoRelativo = caminhoRelativo,
            TamanhoBytes = new FileInfo(caminhoFisico).Length,
            ContentType = MimeTypeFromExtension(extensao),
        };
    }

    public Task<Stream> OpenReadAsync(string caminhoRelativo, CancellationToken ct = default)
    {
        var caminhoFisico = Path.Combine(_options.BasePath, caminhoRelativo);
        Stream stream = File.OpenRead(caminhoFisico);
        return Task.FromResult(stream);
    }

    public Task DeleteAsync(string caminhoRelativo, CancellationToken ct = default)
    {
        var caminhoFisico = Path.Combine(_options.BasePath, caminhoRelativo);
        if (File.Exists(caminhoFisico))
            File.Delete(caminhoFisico);
        return Task.CompletedTask;
    }

    public string ResolvePublicUrl(string caminhoRelativo) =>
        $"{_options.PublicBaseUrl.TrimEnd('/')}/{caminhoRelativo.TrimStart('/')}";

    private static string MimeTypeFromExtension(string extensao) => extensao.ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        ".webp" => "image/webp",
        ".gif" => "image/gif",
        ".pdf" => "application/pdf",
        _ => "application/octet-stream",
    };
}
