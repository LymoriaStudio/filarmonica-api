using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/interessados")]
[Authorize]
public class AdminInteressadosController : ControllerBase
{
    private readonly IInteressadoAdminService _service;

    public AdminInteressadosController(IInteressadoAdminService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<InteressadoAdminDto>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<InteressadoAdminDto>> UpdateStatus(Guid id, UpdateInteressadoStatusRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateStatusAsync(id, request, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
