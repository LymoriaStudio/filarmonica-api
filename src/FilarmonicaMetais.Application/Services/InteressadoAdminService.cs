using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class InteressadoAdminService : IInteressadoAdminService
{
    private readonly IUnitOfWork _uow;

    public InteressadoAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<InteressadoAdminDto>> GetAllAsync(CancellationToken ct = default)
    {
        var interessados = await _uow.Interessados.GetAllAsync(ct);
        return interessados.OrderByDescending(i => i.Data).Select(ToDto).ToList();
    }

    public async Task<InteressadoAdminDto> UpdateStatusAsync(Guid id, UpdateInteressadoStatusRequest request, CancellationToken ct = default)
    {
        var interessado = await _uow.Interessados.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Interessado), id);

        if (!Enum.TryParse<InteressadoStatus>(request.Status, true, out var status))
            throw new ValidationException($"Status '{request.Status}' inválido.");

        interessado.Status = status;
        interessado.UpdatedAt = DateTime.UtcNow;

        _uow.Interessados.Update(interessado);
        await _uow.SaveChangesAsync(ct);

        return ToDto(interessado);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var interessado = await _uow.Interessados.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Interessado), id);
        _uow.Interessados.Remove(interessado);
        await _uow.SaveChangesAsync(ct);
    }

    private static InteressadoAdminDto ToDto(Interessado i) => new()
    {
        Id = i.Id,
        Nome = i.Nome,
        Email = i.Email,
        Telefone = i.Telefone,
        Idade = i.Idade,
        InstrumentoInteresse = i.InstrumentoInteresse,
        Mensagem = i.Mensagem,
        Data = i.Data,
        Status = i.Status.ToString(),
    };
}
