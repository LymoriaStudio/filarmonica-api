using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/media")]
[Authorize]
public class AdminMediaController : ControllerBase
{
    private readonly IMediaAdminService _service;

    public AdminMediaController(IMediaAdminService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<MediaAssetDto>> Upload(IFormFile file, [FromForm] string pasta, CancellationToken ct)
    {
        if (file.Length == 0)
            return BadRequest(new { detail = "Arquivo vazio." });

        await using var stream = file.OpenReadStream();
        var asset = await _service.UploadAsync(stream, file.FileName, file.ContentType, pasta, ct);
        return Ok(asset);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MediaAssetDto>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:guid}/uso")]
    public async Task<ActionResult<MediaUsoDto>> CheckUso(Guid id, CancellationToken ct) =>
        Ok(await _service.CheckUsoAsync(id, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
