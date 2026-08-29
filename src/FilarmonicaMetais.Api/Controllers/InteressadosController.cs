using FilarmonicaMetais.Application.DTOs.Formularios;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/interessados")]
public class InteressadosController : ControllerBase
{
    private readonly IInteressadoService _service;

    public InteressadosController(IInteressadoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CreateInteressadoRequest request, CancellationToken ct)
    {
        var id = await _service.CriarAsync(request, ct);
        return CreatedAtAction(nameof(Criar), new { id }, new { id });
    }
}
