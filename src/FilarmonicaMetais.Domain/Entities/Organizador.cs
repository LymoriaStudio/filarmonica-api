using FilarmonicaMetais.Domain.Common;

namespace FilarmonicaMetais.Domain.Entities;

public class Organizador : AuditableEntity
{
    public string? Foto { get; set; } // caminho relativo
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
