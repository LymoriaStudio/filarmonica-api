namespace FilarmonicaMetais.Application.DTOs.Auth;

public class UsuarioDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? JobTitle { get; set; }
    public string? AvatarUrl { get; set; }
}

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public UsuarioDto Usuario { get; set; } = null!;
}
