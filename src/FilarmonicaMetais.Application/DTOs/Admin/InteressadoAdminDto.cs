namespace FilarmonicaMetais.Application.DTOs.Admin;

public class InteressadoAdminDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int? Idade { get; set; }
    public string InstrumentoInteresse { get; set; } = string.Empty;
    public string? Mensagem { get; set; }
    public DateOnly Data { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateInteressadoStatusRequest
{
    public string Status { get; set; } = string.Empty;
}
