namespace FilarmonicaMetais.Application.DTOs.Admin;

public class AdminInstrumentoFotoDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int Ordem { get; set; }
}

public class AdminInstrumentoDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string DescricaoLonga { get; set; } = string.Empty;
    public string Imagem { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string Cor { get; set; } = string.Empty;
    public IReadOnlyList<AdminInstrumentoFotoDto> Galeria { get; set; } = Array.Empty<AdminInstrumentoFotoDto>();
}
