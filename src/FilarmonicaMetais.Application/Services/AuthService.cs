using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Auth;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;

namespace FilarmonicaMetais.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenService _jwt;

    public AuthService(IUnitOfWork uow, IPasswordHasher hasher, IJwtTokenService jwt)
    {
        _uow = uow;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var usuario = await _uow.Usuarios.GetByEmailAsync(request.Email, ct);

        // Mensagem genérica de propósito — não revela se o e-mail existe ou não.
        if (usuario is null || !usuario.IsActive || !_hasher.Verify(request.Password, usuario.PasswordHash))
            throw new UnauthorizedAppException("E-mail ou senha inválidos.");

        var tokens = _jwt.GerarTokens(usuario);
        usuario.RefreshToken = tokens.RefreshToken;
        usuario.RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt;
        _uow.Usuarios.Update(usuario);
        await _uow.SaveChangesAsync(ct);

        return new LoginResponse
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            Usuario = ToDto(usuario),
        };
    }

    public async Task<LoginResponse> RefreshAsync(string refreshToken, CancellationToken ct = default)
    {
        var usuario = await _uow.Usuarios.GetByRefreshTokenAsync(refreshToken, ct)
            ?? throw new UnauthorizedAppException("Refresh token inválido ou expirado.");

        var tokens = _jwt.GerarTokens(usuario);
        usuario.RefreshToken = tokens.RefreshToken;
        usuario.RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt;
        _uow.Usuarios.Update(usuario);
        await _uow.SaveChangesAsync(ct);

        return new LoginResponse
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            Usuario = ToDto(usuario),
        };
    }

    public async Task LogoutAsync(Guid usuarioId, CancellationToken ct = default)
    {
        var usuario = await _uow.Usuarios.GetByIdAsync(usuarioId, ct);
        if (usuario is null) return;

        usuario.RefreshToken = null;
        usuario.RefreshTokenExpiresAt = null;
        _uow.Usuarios.Update(usuario);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task<UsuarioDto> GetMeAsync(Guid usuarioId, CancellationToken ct = default)
    {
        var usuario = await _uow.Usuarios.GetByIdAsync(usuarioId, ct)
            ?? throw new NotFoundException(nameof(Domain.Entities.Usuario), usuarioId);
        return ToDto(usuario);
    }

    public async Task ChangePasswordAsync(Guid usuarioId, ChangePasswordRequest request, CancellationToken ct = default)
    {
        var usuario = await _uow.Usuarios.GetByIdAsync(usuarioId, ct)
            ?? throw new NotFoundException(nameof(Domain.Entities.Usuario), usuarioId);

        // Exige a senha atual (igual ao modal já implementado no painel React) —
        // evita que um token roubado, sozinho, baste para sequestrar a conta.
        if (!_hasher.Verify(request.CurrentPassword, usuario.PasswordHash))
            throw new ValidationException("Senha atual incorreta.");

        usuario.PasswordHash = _hasher.Hash(request.NewPassword);
        _uow.Usuarios.Update(usuario);
        await _uow.SaveChangesAsync(ct);
    }

    private static UsuarioDto ToDto(Domain.Entities.Usuario u) => new()
    {
        Id = u.Id,
        FullName = u.FullName,
        Email = u.Email,
        Role = u.Role.ToString(),
        JobTitle = u.JobTitle,
        AvatarUrl = u.AvatarUrl,
    };
}
