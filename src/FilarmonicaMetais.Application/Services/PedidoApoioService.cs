using FilarmonicaMetais.Application.DTOs.Formularios;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class PedidoApoioService : IPedidoApoioService
{
    private readonly IUnitOfWork _uow;

    public PedidoApoioService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> CriarAsync(CreatePedidoApoioRequest request, CancellationToken ct = default)
    {
        var pedido = new PedidoApoio
        {
            Nome = request.Nome,
            Empresa = request.Empresa,
            Email = request.Email,
            Telefone = request.Telefone,
            TipoApoio = request.TipoApoio,
            Mensagem = request.Mensagem,
            Data = DateOnly.FromDateTime(DateTime.UtcNow),
            Status = PedidoApoioStatus.Pendente,
        };

        await _uow.PedidosApoio.AddAsync(pedido, ct);
        await _uow.SaveChangesAsync(ct);

        return pedido.Id;
    }
}
