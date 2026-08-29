using FilarmonicaMetais.Domain.Common;

namespace FilarmonicaMetais.Domain.Entities;

// Decisão de migração: consolidado na FK. O front hoje mantém `professor_in_charge`
// como cópia congelada do nome (gravada no mesmo select que escolhe o ProfessorId) —
// isso já causa nome defasado quando um professor é renomeado. Aqui não há esse campo:
// o nome do professor vem sempre por Professor.Nome na leitura.
public class Curso : AuditableEntity
{
    public string? Imagem { get; set; } // caminho relativo
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string FaixaEtaria { get; set; } = string.Empty;
    public string Duracao { get; set; } = string.Empty;
    public int VagasDisponiveis { get; set; }

    public Guid ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;
}
