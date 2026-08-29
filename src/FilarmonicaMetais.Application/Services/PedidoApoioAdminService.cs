using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class PedidoApoioAdminService : IPedidoApoioAdminService
{
    private readonly IUnitOfWork _uow;

    public PedidoApoioAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<PedidoApoioAdminDto>> GetAllAsync(CancellationToken ct = default)
    {
        var pedidos = await _uow.PedidosApoio.GetAllAsync(ct);
        return pedidos.OrderByDescending(p => p.Data).Select(ToDto).ToList();
    }

    public async Task<PedidoApoioAdminDto> UpdateStatusAsync(Guid id, UpdatePedidoApoioStatusRequest request, CancellationToken ct = default)
    {
        var pedido = await _uow.PedidosApoio.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(PedidoApoio), id);

        if (!Enum.TryParse<PedidoApoioStatus>(request.Status, true, out var status))
            throw new ValidationException($"Status '{request.Status}' inválido.");

        pedido.Status = status;
        pedido.UpdatedAt = DateTime.UtcNow;

        _uow.PedidosApoio.Update(pedido);
        await _uow.SaveChangesAsync(ct);

        return ToDto(pedido);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var pedido = await _uow.PedidosApoio.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(PedidoApoio), id);
        _uow.PedidosApoio.Remove(pedido);
        await _uow.SaveChangesAsync(ct);
    }

    private static PedidoApoioAdminDto ToDto(PedidoApoio p) => new()
    {
        Id = p.Id,
        Nome = p.Nome,
        Empresa = p.Empresa,
        Email = p.Email,
        Telefone = p.Telefone,
        TipoApoio = p.TipoApoio,
        Mensagem = p.Mensagem,
        Data = p.Data,
        Status = p.Status.ToString(),
    };
}
