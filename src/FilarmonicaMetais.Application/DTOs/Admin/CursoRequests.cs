namespace FilarmonicaMetais.Application.DTOs.Admin;

public class CreateCursoRequest
{
    public string? Imagem { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string FaixaEtaria { get; set; } = string.Empty;
    public string Duracao { get; set; } = string.Empty;
    public int VagasDisponiveis { get; set; }
    public Guid ProfessorId { get; set; }
}

public class UpdateCursoRequest : CreateCursoRequest
{
}
