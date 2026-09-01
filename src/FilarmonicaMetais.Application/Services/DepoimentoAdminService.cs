using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class DepoimentoAdminService : IDepoimentoAdminService
{
    private readonly IUnitOfWork _uow;

    public DepoimentoAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<AdminDepoimentoDto>> GetAllAsync(CancellationToken ct = default)
    {
        var depoimentos = await _uow.Depoimentos.GetOrdenadosAsync(ct);
        return depoimentos.Select(ToDto).ToList();
    }

    public async Task<AdminDepoimentoDto> CreateAsync(CreateDepoimentoRequest request, CancellationToken ct = default)
    {
        var depoimento = new Depoimento();
        Apply(depoimento, request);

        await _uow.Depoimentos.AddAsync(depoimento, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(depoimento);
    }

    public async Task<AdminDepoimentoDto> UpdateAsync(Guid id, UpdateDepoimentoRequest request, CancellationToken ct = default)
    {
        var depoimento = await _uow.Depoimentos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Depoimento), id);

        Apply(depoimento, request);
        depoimento.UpdatedAt = DateTime.UtcNow;

        _uow.Depoimentos.Update(depoimento);
        await _uow.SaveChangesAsync(ct);

        return ToDto(depoimento);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var depoimento = await _uow.Depoimentos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Depoimento), id);
        _uow.Depoimentos.Remove(depoimento);
        await _uow.SaveChangesAsync(ct);
    }

    private static void Apply(Depoimento d, CreateDepoimentoRequest request)
    {
        d.Nome = request.Nome;
        d.Tag = request.Tag;
        d.TagDetalhe = request.TagDetalhe;
        d.Texto = request.Texto;
        d.DisplayOrder = request.DisplayOrder;
        d.Active = request.Active;
    }

    private static AdminDepoimentoDto ToDto(Depoimento d) => new()
    {
        Id = d.Id,
        Nome = d.Nome,
        Tag = d.Tag,
        TagDetalhe = d.TagDetalhe,
        Texto = d.Texto,
        DisplayOrder = d.DisplayOrder,
        Active = d.Active,
    };
}
