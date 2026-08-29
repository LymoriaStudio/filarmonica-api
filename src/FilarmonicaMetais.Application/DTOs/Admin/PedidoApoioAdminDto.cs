namespace FilarmonicaMetais.Application.DTOs.Admin;

public class PedidoApoioAdminDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Empresa { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string TipoApoio { get; set; } = string.Empty;
    public string? Mensagem { get; set; }
    public DateOnly Data { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdatePedidoApoioStatusRequest
{
    public string Status { get; set; } = string.Empty;
}
