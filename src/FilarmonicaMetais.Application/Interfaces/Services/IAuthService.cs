using FilarmonicaMetais.Application.DTOs.Auth;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<LoginResponse> RefreshAsync(string refreshToken, CancellationToken ct = default);
    Task LogoutAsync(Guid usuarioId, CancellationToken ct = default);
    Task<UsuarioDto> GetMeAsync(Guid usuarioId, CancellationToken ct = default);
    Task ChangePasswordAsync(Guid usuarioId, ChangePasswordRequest request, CancellationToken ct = default);
}
