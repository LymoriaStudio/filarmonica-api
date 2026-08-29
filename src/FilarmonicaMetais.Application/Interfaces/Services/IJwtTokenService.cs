using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public class TokenGerado
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; init; }
}

public interface IJwtTokenService
{
    TokenGerado GerarTokens(Usuario usuario);
    string GerarRefreshToken();
}
