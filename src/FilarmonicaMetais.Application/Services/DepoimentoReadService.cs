using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;

namespace FilarmonicaMetais.Application.Services;

public class DepoimentoReadService : IDepoimentoReadService
{
    private readonly IUnitOfWork _uow;

    public DepoimentoReadService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<DepoimentoDto>> GetAllAsync(CancellationToken ct = default)
    {
        var depoimentos = await _uow.Depoimentos.GetAtivosOrdenadosAsync(ct);
        return depoimentos.Select(d => new DepoimentoDto
        {
            Id = d.Id,
            Nome = d.Nome,
            Tag = d.Tag,
            TagDetalhe = d.TagDetalhe,
            Texto = d.Texto,
        }).ToList();
    }
}
