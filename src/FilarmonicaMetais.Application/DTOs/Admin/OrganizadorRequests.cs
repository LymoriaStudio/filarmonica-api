namespace FilarmonicaMetais.Application.DTOs.Admin;

public class CreateOrganizadorRequest
{
    public string? Foto { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UpdateOrganizadorRequest : CreateOrganizadorRequest
{
}
