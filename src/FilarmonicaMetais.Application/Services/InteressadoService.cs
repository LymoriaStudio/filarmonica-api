using FilarmonicaMetais.Application.DTOs.Formularios;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class InteressadoService : IInteressadoService
{
    private readonly IUnitOfWork _uow;

    public InteressadoService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> CriarAsync(CreateInteressadoRequest request, CancellationToken ct = default)
    {
        var interessado = new Interessado
        {
            Nome = request.Nome,
            Email = request.Email,
            Telefone = request.Telefone,
            Idade = request.Idade,
            InstrumentoInteresse = request.InstrumentoInteresse,
            Mensagem = request.Mensagem,
            Data = DateOnly.FromDateTime(DateTime.UtcNow),
            Status = InteressadoStatus.Novo,
        };

        await _uow.Interessados.AddAsync(interessado, ct);
        await _uow.SaveChangesAsync(ct);

        return interessado.Id;
    }
}
