namespace FilarmonicaMetais.Application.DTOs.Auth;

// Perfil autoatendido: só nome/e-mail/avatar. Cargo (Role) fica de fora de propósito —
// diferente do updateMyProfile() antigo do Supabase, que deixava o próprio usuário
// trocar sua role. Mudar role é exclusivo de /api/admin/usuarios (AdminOnly).
public class UpdateMeRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
}
