using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/banners")]
public class BannersController : ControllerBase
{
    private readonly IBannerReadService _service;

    public BannersController(IBannerReadService service)
    {
        _service = service;
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IReadOnlyList<BannerDto>>> GetAtivos(CancellationToken ct)
    {
        var banners = await _service.GetAtivosAsync(ct);
        return Ok(banners);
    }
}
