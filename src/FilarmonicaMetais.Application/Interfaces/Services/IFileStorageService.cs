namespace FilarmonicaMetais.Application.Interfaces.Services;

public class ArquivoArmazenado
{
    // Caminho RELATIVO (ex: "instruments/1699-trompete.jpg") — nunca URL absoluta.
    // Ver decisão de projeto no plano de migração: trocar de host vira uma variável
    // de ambiente (FileStorage:PublicBaseUrl), não um UPDATE em massa no banco.
    public string CaminhoRelativo { get; init; } = string.Empty;
    public long TamanhoBytes { get; init; }
    public string ContentType { get; init; } = string.Empty;
}

// Abstrai onde/como os arquivos ficam gravados. A implementação inicial usa disco local
// (volume do Railway); trocar de estratégia no futuro (ex: pasta de rede do cliente)
// não deve afetar quem consome esta interface.
public interface IFileStorageService
{
    Task<ArquivoArmazenado> SaveAsync(Stream conteudo, string nomeOriginal, string pasta, CancellationToken ct = default);
    Task<Stream> OpenReadAsync(string caminhoRelativo, CancellationToken ct = default);
    Task DeleteAsync(string caminhoRelativo, CancellationToken ct = default);

    // Compõe a URL pública a partir do caminho relativo + FileStorage:PublicBaseUrl.
    string ResolvePublicUrl(string caminhoRelativo);
}
