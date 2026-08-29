using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.DTOs.Common;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/alunos")]
[Authorize]
public class AdminAlunosController : ControllerBase
{
    private readonly IAlunoAdminService _service;

    public AdminAlunosController(IAlunoAdminService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<AdminAlunoDto>>> GetPaginado(
        [FromQuery] string? busca,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default) =>
        Ok(await _service.BuscarPaginadoAsync(busca, status, page, pageSize, ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AdminAlunoDto>> GetById(Guid id, CancellationToken ct) =>
        Ok(await _service.GetByIdAsync(id, ct));

    [HttpPost]
    public async Task<ActionResult<AdminAlunoDto>> Create(CreateAlunoRequest request, CancellationToken ct)
    {
        var criado = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AdminAlunoDto>> Update(Guid id, UpdateAlunoRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateAsync(id, request, ct));

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<AdminAlunoDto>> UpdateStatus(Guid id, UpdateAlunoStatusRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateStatusAsync(id, request, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
