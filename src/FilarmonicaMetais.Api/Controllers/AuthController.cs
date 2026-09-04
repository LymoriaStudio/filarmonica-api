using System.Security.Claims;
using FilarmonicaMetais.Application.DTOs.Auth;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken ct)
    {
        var response = await _authService.LoginAsync(request, ct);
        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse>> Refresh(RefreshTokenRequest request, CancellationToken ct)
    {
        var response = await _authService.RefreshAsync(request.RefreshToken, ct);
        return Ok(response);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(CancellationToken ct)
    {
        await _authService.LogoutAsync(GetUsuarioId(), ct);
        return NoContent();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UsuarioDto>> Me(CancellationToken ct)
    {
        var usuario = await _authService.GetMeAsync(GetUsuarioId(), ct);
        return Ok(usuario);
    }

    [HttpPut("me")]
    [Authorize]
    public async Task<ActionResult<UsuarioDto>> UpdateMe(UpdateMeRequest request, CancellationToken ct)
    {
        var usuario = await _authService.UpdateMeAsync(GetUsuarioId(), request, ct);
        return Ok(usuario);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken ct)
    {
        await _authService.ChangePasswordAsync(GetUsuarioId(), request, ct);
        return NoContent();
    }

    private Guid GetUsuarioId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub")!);
}
