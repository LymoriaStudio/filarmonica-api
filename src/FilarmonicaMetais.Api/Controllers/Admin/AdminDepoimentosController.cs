using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/depoimentos")]
[Authorize]
public class AdminDepoimentosController : ControllerBase
{
    private readonly IDepoimentoAdminService _service;

    public AdminDepoimentosController(IDepoimentoAdminService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AdminDepoimentoDto>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpPost]
    public async Task<IActionResult> Create(CreateDepoimentoRequest request, CancellationToken ct)
    {
        var criado = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(Create), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateDepoimentoRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateAsync(id, request, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
