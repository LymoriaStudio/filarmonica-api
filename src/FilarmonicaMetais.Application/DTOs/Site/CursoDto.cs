namespace FilarmonicaMetais.Application.DTOs.Site;

public class CursoDto
{
    public Guid Id { get; set; }
    public string? ImagemUrl { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string FaixaEtaria { get; set; } = string.Empty;
    public string Duracao { get; set; } = string.Empty;
    public int VagasDisponiveis { get; set; }

    // Composto por navigation property (Professor.Nome) — decisão de migração
    // que consolidou a FK e eliminou o campo de texto duplicado que existia no Supabase.
    public Guid ProfessorId { get; set; }
    public string ProfessorNome { get; set; } = string.Empty;
}
