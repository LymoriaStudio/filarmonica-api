using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class DoacaoAdminService : IDoacaoAdminService
{
    private readonly IUnitOfWork _uow;

    public DoacaoAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<DoacaoAdminDto>> GetAllAsync(CancellationToken ct = default)
    {
        var doacoes = await _uow.Doacoes.GetAllAsync(ct);
        return doacoes.OrderByDescending(d => d.Data).Select(ToDto).ToList();
    }

    public async Task<DoacaoAdminDto> CreateAsync(CreateDoacaoRequest request, CancellationToken ct = default)
    {
        var doacao = new Doacao { Data = DateOnly.FromDateTime(DateTime.UtcNow) };
        Apply(doacao, request);

        await _uow.Doacoes.AddAsync(doacao, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(doacao);
    }

    public async Task<DoacaoAdminDto> UpdateAsync(Guid id, UpdateDoacaoRequest request, CancellationToken ct = default)
    {
        var doacao = await _uow.Doacoes.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Doacao), id);

        Apply(doacao, request);
        doacao.UpdatedAt = DateTime.UtcNow;

        _uow.Doacoes.Update(doacao);
        await _uow.SaveChangesAsync(ct);

        return ToDto(doacao);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var doacao = await _uow.Doacoes.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Doacao), id);
        _uow.Doacoes.Remove(doacao);
        await _uow.SaveChangesAsync(ct);
    }

    private static void Apply(Doacao doacao, CreateDoacaoRequest request)
    {
        doacao.NomeDoador = request.NomeDoador;
        doacao.TipoDoador = Enum.TryParse<TipoDoador>(request.TipoDoador, true, out var tipo) ? tipo : TipoDoador.Fisica;
        doacao.CpfCnpj = request.CpfCnpj;
        doacao.EmailDoador = request.EmailDoador;
        doacao.Valor = request.Valor;
        doacao.Status = Enum.TryParse<DoacaoStatus>(request.Status, true, out var status) ? status : DoacaoStatus.Pendente;
    }

    private static DoacaoAdminDto ToDto(Doacao d) => new()
    {
        Id = d.Id,
        NomeDoador = d.NomeDoador,
        TipoDoador = d.TipoDoador.ToString(),
        CpfCnpj = d.CpfCnpj,
        EmailDoador = d.EmailDoador,
        Valor = d.Valor,
        Data = d.Data,
        Status = d.Status.ToString(),
    };
}
