using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/depoimentos")]
public class DepoimentosController : ControllerBase
{
    private readonly IDepoimentoReadService _service;

    public DepoimentosController(IDepoimentoReadService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DepoimentoDto>>> GetAll(CancellationToken ct)
    {
        var depoimentos = await _service.GetAllAsync(ct);
        return Ok(depoimentos);
    }
}
