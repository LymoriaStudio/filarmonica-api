using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/instrumentos")]
public class InstrumentosController : ControllerBase
{
    private readonly IInstrumentoReadService _service;

    public InstrumentosController(IInstrumentoReadService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<InstrumentoDto>>> GetAll(CancellationToken ct)
    {
        var instrumentos = await _service.GetAllAsync(ct);
        return Ok(instrumentos);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<InstrumentoDto>> GetBySlug(string slug, CancellationToken ct)
    {
        var instrumento = await _service.GetBySlugAsync(slug, ct);
        return instrumento is null ? NotFound() : Ok(instrumento);
    }
}
