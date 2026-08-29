namespace FilarmonicaMetais.Application.DTOs.Admin;

public class AdminAlunoDto
{
    public Guid Id { get; set; }
    public string? Foto { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateOnly? DataNascimento { get; set; }
    public string Instrumento { get; set; } = string.Empty;
    public string? Turma { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Responsavel { get; set; }
    public string Status { get; set; } = string.Empty;

    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Uf { get; set; }
}
