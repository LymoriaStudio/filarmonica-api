namespace FilarmonicaMetais.Application.DTOs.Admin;

public class MediaAssetDto
{
    public Guid Id { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public string NomeOriginal { get; set; } = string.Empty;
    public string CaminhoRelativo { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Pasta { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long TamanhoBytes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class MediaUsoDto
{
    public bool EmUso { get; set; }
    public IReadOnlyList<string> UsadoPor { get; set; } = Array.Empty<string>();
}
