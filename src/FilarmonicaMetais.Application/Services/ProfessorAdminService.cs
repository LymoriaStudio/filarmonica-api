using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class ProfessorAdminService : IProfessorAdminService
{
    private readonly IUnitOfWork _uow;

    public ProfessorAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<AdminProfessorDto>> GetAllAsync(CancellationToken ct = default)
    {
        var professores = await _uow.Professores.GetOrdenadosAsync(ct);
        return professores.Select(ToDto).ToList();
    }

    public async Task<AdminProfessorDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var professor = await _uow.Professores.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Professor), id);
        return ToDto(professor);
    }

    public async Task<AdminProfessorDto> CreateAsync(CreateProfessorRequest request, CancellationToken ct = default)
    {
        var professor = new Professor();
        Apply(professor, request);

        await _uow.Professores.AddAsync(professor, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(professor);
    }

    public async Task<AdminProfessorDto> UpdateAsync(Guid id, UpdateProfessorRequest request, CancellationToken ct = default)
    {
        var professor = await _uow.Professores.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Professor), id);

        Apply(professor, request);
        professor.UpdatedAt = DateTime.UtcNow;

        _uow.Professores.Update(professor);
        await _uow.SaveChangesAsync(ct);

        return ToDto(professor);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var professor = await _uow.Professores.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Professor), id);
        _uow.Professores.Remove(professor);
        await _uow.SaveChangesAsync(ct);
    }

    private static void Apply(Professor professor, CreateProfessorRequest request)
    {
        professor.Foto = string.IsNullOrWhiteSpace(request.Foto) ? null : request.Foto;
        professor.Nome = request.Nome;
        professor.Cargo = request.Cargo;
        professor.Especialidade = request.Especialidade;
        professor.Instrumento = request.Instrumento;
        professor.MiniBio = request.MiniBio;
        professor.BioCompleta = request.BioCompleta;
        professor.Redes.Instagram = request.Instagram;
        professor.Redes.Facebook = request.Facebook;
        professor.Redes.Youtube = request.Youtube;
        professor.Redes.Linkedin = request.Linkedin;
        professor.Redes.Whatsapp = request.Whatsapp;
        professor.Email = request.Email;
        professor.Telefone = request.Telefone;
        professor.Destaque = request.Destaque;
        professor.DisplayOrder = request.DisplayOrder;
    }

    private static AdminProfessorDto ToDto(Professor p) => new()
    {
        Id = p.Id,
        Foto = p.Foto,
        Nome = p.Nome,
        Cargo = p.Cargo,
        Especialidade = p.Especialidade,
        Instrumento = p.Instrumento,
        MiniBio = p.MiniBio,
        BioCompleta = p.BioCompleta,
        Instagram = p.Redes.Instagram,
        Facebook = p.Redes.Facebook,
        Youtube = p.Redes.Youtube,
        Linkedin = p.Redes.Linkedin,
        Whatsapp = p.Redes.Whatsapp,
        Email = p.Email,
        Telefone = p.Telefone,
        Destaque = p.Destaque,
        DisplayOrder = p.DisplayOrder,
    };
}
