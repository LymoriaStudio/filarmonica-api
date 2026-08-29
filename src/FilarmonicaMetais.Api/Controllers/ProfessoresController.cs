using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/professores")]
public class ProfessoresController : ControllerBase
{
    private readonly IProfessorReadService _service;

    public ProfessoresController(IProfessorReadService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProfessorDto>>> GetAll(CancellationToken ct)
    {
        var professores = await _service.GetAllAsync(ct);
        return Ok(professores);
    }
}
