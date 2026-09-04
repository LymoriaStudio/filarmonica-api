using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/audit-logs")]
[Authorize]
public class AdminAuditLogsController : ControllerBase
{
    private readonly IAuditLogService _service;

    public AdminAuditLogsController(IAuditLogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AuditLogDto>>> GetRecentes([FromQuery] int limit = 100, CancellationToken ct = default) =>
        Ok(await _service.GetRecentesAsync(limit, ct));

    [HttpPost]
    public async Task<IActionResult> Log(CreateAuditLogRequest request, CancellationToken ct)
    {
        await _service.LogAsync(request, ct);
        return NoContent();
    }
}
