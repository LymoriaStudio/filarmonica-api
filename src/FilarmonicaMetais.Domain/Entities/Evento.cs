using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Domain.Entities;

public class Evento : AuditableEntity
{
    public string? ImagemCapa { get; set; } // caminho relativo
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateOnly Data { get; set; }
    public TimeOnly? Horario { get; set; }
    public string Local { get; set; } = string.Empty;
    public string? Endereco { get; set; }
    public string? GoogleMapsUrl { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public EventoStatus Status { get; set; } = EventoStatus.Rascunho;
    public bool Destaque { get; set; }
    public string? Link { get; set; }
    public bool Pago { get; set; }
    public decimal? ValorIngresso { get; set; } // precisão fixada na configuration: HasPrecision(18,2)
}
