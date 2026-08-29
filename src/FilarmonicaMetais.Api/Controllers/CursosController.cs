using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/cursos")]
public class CursosController : ControllerBase
{
    private readonly ICursoReadService _service;

    public CursosController(ICursoReadService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CursoDto>>> GetAll(CancellationToken ct)
    {
        var cursos = await _service.GetAllAsync(ct);
        return Ok(cursos);
    }
}
