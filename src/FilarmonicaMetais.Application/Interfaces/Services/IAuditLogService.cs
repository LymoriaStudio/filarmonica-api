using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IAuditLogService
{
    Task<IReadOnlyList<AuditLogDto>> GetRecentesAsync(int limit, CancellationToken ct = default);
    Task LogAsync(CreateAuditLogRequest request, CancellationToken ct = default);
}
