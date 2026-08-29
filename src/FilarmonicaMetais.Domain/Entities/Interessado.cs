using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Domain.Entities;

public class Interessado : AuditableEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int? Idade { get; set; }
    public string InstrumentoInteresse { get; set; } = string.Empty;
    public string? Mensagem { get; set; }
    public DateOnly Data { get; set; }
    public InteressadoStatus Status { get; set; } = InteressadoStatus.Novo;
}
