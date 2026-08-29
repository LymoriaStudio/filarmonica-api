using FilarmonicaMetais.Application.DTOs.Common;
using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/eventos")]
public class EventosController : ControllerBase
{
    private readonly IEventoReadService _service;

    public EventosController(IEventoReadService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<EventoDto>>> GetPaginado(
        [FromQuery] string? categoria,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var resultado = await _service.BuscarPaginadoAsync(categoria, status, page, pageSize, ct);
        return Ok(resultado);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EventoDto>> GetById(Guid id, CancellationToken ct)
    {
        var evento = await _service.GetByIdAsync(id, ct);
        return evento is null ? NotFound() : Ok(evento);
    }
}
