using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

// AdminOnly: doações lidam com dados financeiros — mesma restrição que a Sidebar
// e o render do painel já aplicam no front (só admin vê "Financeiro"), agora
// também no servidor, que é onde a regra de fato passa a valer.
[ApiController]
[Route("api/admin/doacoes")]
[Authorize(Policy = "AdminOnly")]
public class AdminDoacoesController : ControllerBase
{
    private readonly IDoacaoAdminService _service;

    public AdminDoacoesController(IDoacaoAdminService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DoacaoAdminDto>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpPost]
    public async Task<ActionResult<DoacaoAdminDto>> Create(CreateDoacaoRequest request, CancellationToken ct)
    {
        var criada = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetAll), criada);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DoacaoAdminDto>> Update(Guid id, UpdateDoacaoRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateAsync(id, request, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
