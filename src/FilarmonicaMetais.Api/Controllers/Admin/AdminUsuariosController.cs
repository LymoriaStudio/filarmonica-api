using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.DTOs.Auth;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

// AdminOnly: só admin cria/edita/apaga contas do painel — equivalente ao
// "Controle Usuários" restrito por role no Sidebar do front.
[ApiController]
[Route("api/admin/usuarios")]
[Authorize(Policy = "AdminOnly")]
public class AdminUsuariosController : ControllerBase
{
    private readonly IUsuarioAdminService _service;

    public AdminUsuariosController(IUsuarioAdminService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UsuarioDto>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Create(CreateUsuarioRequest request, CancellationToken ct)
    {
        var criado = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetAll), criado);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UsuarioDto>> Update(Guid id, UpdateUsuarioRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateAsync(id, request, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
