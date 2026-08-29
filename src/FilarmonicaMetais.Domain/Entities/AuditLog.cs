using FilarmonicaMetais.Domain.Common;

namespace FilarmonicaMetais.Domain.Entities;

// Desnormalizado de propósito: grava e-mail/nome, não FK para Usuario —
// o log de auditoria deve sobreviver à exclusão do usuário.
public class AuditLog : BaseEntity
{
    public string UserEmail { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty;
    public string? Details { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}
