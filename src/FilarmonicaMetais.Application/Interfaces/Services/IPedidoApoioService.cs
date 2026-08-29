using FilarmonicaMetais.Application.DTOs.Formularios;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IPedidoApoioService
{
    Task<Guid> CriarAsync(CreatePedidoApoioRequest request, CancellationToken ct = default);
}
