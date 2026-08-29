using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Domain.Entities;

// Funde auth.users + profiles do Supabase numa entidade só.
public class Usuario : AuditableEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Editor;
    public string? JobTitle { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
}
