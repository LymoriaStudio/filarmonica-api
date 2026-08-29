using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.Enums;
using FilarmonicaMetais.Domain.ValueObjects;

namespace FilarmonicaMetais.Domain.Entities;

public class Aluno : AuditableEntity
{
    public string? Foto { get; set; } // caminho relativo
    public string Nome { get; set; } = string.Empty;
    public DateOnly? DataNascimento { get; set; }
    public string Instrumento { get; set; } = string.Empty;
    public string? Turma { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Responsavel { get; set; }
    public AlunoStatus Status { get; set; } = AlunoStatus.Ativo;
    public Endereco Endereco { get; set; } = new();
}
