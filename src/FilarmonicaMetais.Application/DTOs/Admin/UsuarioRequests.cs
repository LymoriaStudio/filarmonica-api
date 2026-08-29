namespace FilarmonicaMetais.Application.DTOs.Admin;

public class CreateUsuarioRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Editor";
    public string? JobTitle { get; set; }
}

public class UpdateUsuarioRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "Editor";
    public string? JobTitle { get; set; }
    public bool IsActive { get; set; } = true;
}
