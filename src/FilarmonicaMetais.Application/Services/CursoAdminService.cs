using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class CursoAdminService : ICursoAdminService
{
    private readonly IUnitOfWork _uow;

    public CursoAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<AdminCursoDto>> GetAllAsync(CancellationToken ct = default)
    {
        var cursos = await _uow.Cursos.GetAllComProfessorAsync(ct);
        return cursos.Select(ToDto).ToList();
    }

    public async Task<AdminCursoDto> CreateAsync(CreateCursoRequest request, CancellationToken ct = default)
    {
        await GarantirProfessorExisteAsync(request.ProfessorId, ct);

        var curso = new Curso();
        Apply(curso, request);

        await _uow.Cursos.AddAsync(curso, ct);
        await _uow.SaveChangesAsync(ct);

        var criado = await _uow.Cursos.GetByIdComProfessorAsync(curso.Id, ct);
        return ToDto(criado!);
    }

    public async Task<AdminCursoDto> UpdateAsync(Guid id, UpdateCursoRequest request, CancellationToken ct = default)
    {
        var curso = await _uow.Cursos.GetByIdComProfessorAsync(id, ct) ?? throw new NotFoundException(nameof(Curso), id);
        await GarantirProfessorExisteAsync(request.ProfessorId, ct);

        Apply(curso, request);
        curso.UpdatedAt = DateTime.UtcNow;

        _uow.Cursos.Update(curso);
        await _uow.SaveChangesAsync(ct);

        var atualizado = await _uow.Cursos.GetByIdComProfessorAsync(id, ct);
        return ToDto(atualizado!);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var curso = await _uow.Cursos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Curso), id);
        _uow.Cursos.Remove(curso);
        await _uow.SaveChangesAsync(ct);
    }

    private async Task GarantirProfessorExisteAsync(Guid professorId, CancellationToken ct)
    {
        if (await _uow.Professores.GetByIdAsync(professorId, ct) is null)
            throw new ValidationException($"Professor com id '{professorId}' não encontrado.");
    }

    private static void Apply(Curso curso, CreateCursoRequest request)
    {
        curso.Imagem = string.IsNullOrWhiteSpace(request.Imagem) ? null : request.Imagem;
        curso.Nome = request.Nome;
        curso.Descricao = request.Descricao;
        curso.FaixaEtaria = request.FaixaEtaria;
        curso.Duracao = request.Duracao;
        curso.VagasDisponiveis = request.VagasDisponiveis;
        curso.ProfessorId = request.ProfessorId;
    }

    private static AdminCursoDto ToDto(Curso c) => new()
    {
        Id = c.Id,
        Imagem = c.Imagem,
        Nome = c.Nome,
        Descricao = c.Descricao,
        FaixaEtaria = c.FaixaEtaria,
        Duracao = c.Duracao,
        VagasDisponiveis = c.VagasDisponiveis,
        ProfessorId = c.ProfessorId,
        ProfessorNome = c.Professor.Nome,
    };
}
