using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Domain.Entities;

// De quero_apoiar no Supabase.
public class PedidoApoio : AuditableEntity
{
    public string Nome { get; set; } = string.Empty;
    public string? Empresa { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string TipoApoio { get; set; } = string.Empty;
    public string? Mensagem { get; set; }
    public DateOnly Data { get; set; }
    public PedidoApoioStatus Status { get; set; } = PedidoApoioStatus.Pendente;
}
