namespace FilarmonicaMetais.Application.DTOs.Formularios;

public class CreatePedidoApoioRequest
{
    public string Nome { get; set; } = string.Empty;
    public string? Empresa { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string TipoApoio { get; set; } = string.Empty;
    public string? Mensagem { get; set; }
}
