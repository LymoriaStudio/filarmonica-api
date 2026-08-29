namespace FilarmonicaMetais.Application.DTOs.Formularios;

public class CreateInteressadoRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int? Idade { get; set; }
    public string InstrumentoInteresse { get; set; } = string.Empty;
    public string? Mensagem { get; set; }
}
