using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.ValueObjects;

namespace FilarmonicaMetais.Domain.Entities;

public class Professor : AuditableEntity
{
    public string? Foto { get; set; } // caminho relativo
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string? Especialidade { get; set; }
    public string Instrumento { get; set; } = string.Empty;
    public string MiniBio { get; set; } = string.Empty;
    public string BioCompleta { get; set; } = string.Empty;
    public RedesSociais Redes { get; set; } = new();
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public bool Destaque { get; set; }
    public int DisplayOrder { get; set; }

    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
