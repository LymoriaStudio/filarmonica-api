using FilarmonicaMetais.Application.DTOs.Formularios;
using FilarmonicaMetais.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilarmonicaMetais.Api.Controllers;

[ApiController]
[Route("api/pedidos-apoio")]
public class PedidosApoioController : ControllerBase
{
    private readonly IPedidoApoioService _service;

    public PedidosApoioController(IPedidoApoioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CreatePedidoApoioRequest request, CancellationToken ct)
    {
        var id = await _service.CriarAsync(request, ct);
        return CreatedAtAction(nameof(Criar), new { id }, new { id });
    }
}
