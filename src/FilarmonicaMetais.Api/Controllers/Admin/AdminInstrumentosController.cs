using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/instrumentos")]
[Authorize]
public class AdminInstrumentosController : ControllerBase
{
    private readonly IInstrumentoAdminService _service;

    public AdminInstrumentosController(IInstrumentoAdminService service)
    {
        _service = service;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AdminInstrumentoDto>> GetById(Guid id, CancellationToken ct) =>
        Ok(await _service.GetByIdAsync(id, ct));

    [HttpPost]
    public async Task<ActionResult<AdminInstrumentoDto>> Create(CreateInstrumentoRequest request, CancellationToken ct)
    {
        var criado = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AdminInstrumentoDto>> Update(Guid id, UpdateInstrumentoRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateAsync(id, request, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/fotos")]
    public async Task<ActionResult<AdminInstrumentoDto>> AddFoto(Guid id, AddInstrumentoFotoRequest request, CancellationToken ct) =>
        Ok(await _service.AddFotoAsync(id, request, ct));

    [HttpDelete("{id:guid}/fotos/{fotoId:guid}")]
    public async Task<ActionResult<AdminInstrumentoDto>> RemoveFoto(Guid id, Guid fotoId, CancellationToken ct) =>
        Ok(await _service.RemoveFotoAsync(id, fotoId, ct));
}
