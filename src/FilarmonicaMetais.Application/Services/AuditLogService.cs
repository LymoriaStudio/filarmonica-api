using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUser;

    public AuditLogService(IUnitOfWork uow, ICurrentUserService currentUser)
    {
        _uow = uow;
        _currentUser = currentUser;
    }

    public async Task<IReadOnlyList<AuditLogDto>> GetRecentesAsync(int limit, CancellationToken ct = default)
    {
        var logs = await _uow.AuditLogs.GetRecentesAsync(limit, ct);
        return logs.Select(ToDto).ToList();
    }

    // user_email/user_name vêm do token autenticado, nunca do corpo da requisição —
    // um usuário não pode forjar o log em nome de outro.
    public async Task LogAsync(CreateAuditLogRequest request, CancellationToken ct = default)
    {
        if (_currentUser.UserId is not { } userId) return;

        var usuario = await _uow.Usuarios.GetByIdAsync(userId, ct);
        if (usuario is null) return;

        var log = new AuditLog
        {
            UserEmail = usuario.Email,
            UserName = usuario.FullName,
            Action = request.Action,
            Module = request.Module,
            Details = request.Details,
            DateTime = DateTime.UtcNow,
        };

        await _uow.AuditLogs.AddAsync(log, ct);
        await _uow.SaveChangesAsync(ct);
    }

    private static AuditLogDto ToDto(AuditLog a) => new()
    {
        Id = a.Id,
        UserEmail = a.UserEmail,
        UserName = a.UserName,
        Action = a.Action,
        Module = a.Module,
        Details = a.Details,
        DateTime = a.DateTime,
    };
}
