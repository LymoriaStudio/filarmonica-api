using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class OrganizadorAdminService : IOrganizadorAdminService
{
    private readonly IUnitOfWork _uow;

    public OrganizadorAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<AdminOrganizadorDto>> GetAllAsync(CancellationToken ct = default)
    {
        var organizadores = await _uow.Organizadores.GetOrdenadosAsync(ct);
        return organizadores.Select(ToDto).ToList();
    }

    public async Task<AdminOrganizadorDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var organizador = await _uow.Organizadores.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Organizador), id);
        return ToDto(organizador);
    }

    public async Task<AdminOrganizadorDto> CreateAsync(CreateOrganizadorRequest request, CancellationToken ct = default)
    {
        var organizador = new Organizador();
        Apply(organizador, request);

        await _uow.Organizadores.AddAsync(organizador, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(organizador);
    }

    public async Task<AdminOrganizadorDto> UpdateAsync(Guid id, UpdateOrganizadorRequest request, CancellationToken ct = default)
    {
        var organizador = await _uow.Organizadores.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Organizador), id);

        Apply(organizador, request);
        organizador.UpdatedAt = DateTime.UtcNow;

        _uow.Organizadores.Update(organizador);
        await _uow.SaveChangesAsync(ct);

        return ToDto(organizador);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var organizador = await _uow.Organizadores.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Organizador), id);
        _uow.Organizadores.Remove(organizador);
        await _uow.SaveChangesAsync(ct);
    }

    private static void Apply(Organizador organizador, CreateOrganizadorRequest request)
    {
        organizador.Foto = string.IsNullOrWhiteSpace(request.Foto) ? null : request.Foto;
        organizador.Nome = request.Nome;
        organizador.Cargo = request.Cargo;
        organizador.Bio = request.Bio;
        organizador.Telefone = request.Telefone;
        organizador.Email = request.Email;
    }

    private static AdminOrganizadorDto ToDto(Organizador o) => new()
    {
        Id = o.Id,
        Foto = o.Foto,
        Nome = o.Nome,
        Cargo = o.Cargo,
        Bio = o.Bio,
        Telefone = o.Telefone,
        Email = o.Email,
    };
}
