namespace FilarmonicaMetais.Application.DTOs.Admin;

// Expõe o caminho relativo cru de ImagemCapa (não a URL resolvida) — mesmo motivo do AdminBannerDto.
public class AdminEventoDto
{
    public Guid Id { get; set; }
    public string? ImagemCapa { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateOnly Data { get; set; }
    public TimeOnly? Horario { get; set; }
    public string Local { get; set; } = string.Empty;
    public string? Endereco { get; set; }
    public string? GoogleMapsUrl { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool Destaque { get; set; }
    public string? Link { get; set; }
    public bool Pago { get; set; }
    public decimal? ValorIngresso { get; set; }
}
