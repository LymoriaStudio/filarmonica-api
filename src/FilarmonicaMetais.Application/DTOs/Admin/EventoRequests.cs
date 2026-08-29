namespace FilarmonicaMetais.Application.DTOs.Admin;

public class CreateEventoRequest
{
    public string? ImagemCapa { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateOnly Data { get; set; }
    public TimeOnly? Horario { get; set; }
    public string Local { get; set; } = string.Empty;
    public string? Endereco { get; set; }
    public string? GoogleMapsUrl { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public string Status { get; set; } = "Rascunho";
    public bool Destaque { get; set; }
    public string? Link { get; set; }
    public bool Pago { get; set; }
    public decimal? ValorIngresso { get; set; }
}

public class UpdateEventoRequest : CreateEventoRequest
{
}
