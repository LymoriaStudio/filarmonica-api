namespace FilarmonicaMetais.Application.DTOs.Site;

public class InstrumentoDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string DescricaoLonga { get; set; } = string.Empty;
    public string ImagemUrl { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string Cor { get; set; } = string.Empty;
    public IReadOnlyList<string> GaleriaUrls { get; set; } = Array.Empty<string>();
}
