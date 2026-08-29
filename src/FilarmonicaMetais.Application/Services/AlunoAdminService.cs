using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.DTOs.Common;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class AlunoAdminService : IAlunoAdminService
{
    private readonly IUnitOfWork _uow;

    public AlunoAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PagedResult<AdminAlunoDto>> BuscarPaginadoAsync(
        string? busca, string? status, int page, int pageSize, CancellationToken ct = default)
    {
        AlunoStatus? statusEnum = Enum.TryParse<AlunoStatus>(status, true, out var parsed) ? parsed : null;
        var (items, totalCount) = await _uow.Alunos.BuscarPaginadoAsync(busca, statusEnum, page, pageSize, ct);

        return new PagedResult<AdminAlunoDto>
        {
            Items = items.Select(ToDto).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }

    public async Task<AdminAlunoDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var aluno = await _uow.Alunos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Aluno), id);
        return ToDto(aluno);
    }

    public async Task<AdminAlunoDto> CreateAsync(CreateAlunoRequest request, CancellationToken ct = default)
    {
        var aluno = new Aluno();
        Apply(aluno, request);

        await _uow.Alunos.AddAsync(aluno, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(aluno);
    }

    public async Task<AdminAlunoDto> UpdateAsync(Guid id, UpdateAlunoRequest request, CancellationToken ct = default)
    {
        var aluno = await _uow.Alunos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Aluno), id);

        Apply(aluno, request);
        aluno.UpdatedAt = DateTime.UtcNow;

        _uow.Alunos.Update(aluno);
        await _uow.SaveChangesAsync(ct);

        return ToDto(aluno);
    }

    // Endpoint dedicado (PATCH) — replica o botão "Arquivar"/"Desarquivar" do painel,
    // que troca só o status sem passar pelo formulário completo de edição.
    public async Task<AdminAlunoDto> UpdateStatusAsync(Guid id, UpdateAlunoStatusRequest request, CancellationToken ct = default)
    {
        var aluno = await _uow.Alunos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Aluno), id);

        if (!Enum.TryParse<AlunoStatus>(request.Status, true, out var status))
            throw new ValidationException($"Status '{request.Status}' inválido.");

        aluno.Status = status;
        aluno.UpdatedAt = DateTime.UtcNow;

        _uow.Alunos.Update(aluno);
        await _uow.SaveChangesAsync(ct);

        return ToDto(aluno);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var aluno = await _uow.Alunos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Aluno), id);
        _uow.Alunos.Remove(aluno);
        await _uow.SaveChangesAsync(ct);
    }

    private static void Apply(Aluno aluno, CreateAlunoRequest request)
    {
        aluno.Foto = string.IsNullOrWhiteSpace(request.Foto) ? null : request.Foto;
        aluno.Nome = request.Nome;
        aluno.DataNascimento = request.DataNascimento;
        aluno.Instrumento = request.Instrumento;
        aluno.Turma = request.Turma;
        aluno.Telefone = request.Telefone;
        aluno.Email = request.Email;
        aluno.Responsavel = request.Responsavel;
        aluno.Status = Enum.TryParse<AlunoStatus>(request.Status, true, out var status) ? status : AlunoStatus.Ativo;

        aluno.Endereco.Cep = request.Cep;
        aluno.Endereco.Logradouro = request.Logradouro;
        aluno.Endereco.Numero = request.Numero;
        aluno.Endereco.Complemento = request.Complemento;
        aluno.Endereco.Bairro = request.Bairro;
        aluno.Endereco.Cidade = request.Cidade;
        aluno.Endereco.Uf = request.Uf;
    }

    private static AdminAlunoDto ToDto(Aluno a) => new()
    {
        Id = a.Id,
        Foto = a.Foto,
        Nome = a.Nome,
        DataNascimento = a.DataNascimento,
        Instrumento = a.Instrumento,
        Turma = a.Turma,
        Telefone = a.Telefone,
        Email = a.Email,
        Responsavel = a.Responsavel,
        Status = a.Status.ToString(),
        Cep = a.Endereco.Cep,
        Logradouro = a.Endereco.Logradouro,
        Numero = a.Endereco.Numero,
        Complemento = a.Endereco.Complemento,
        Bairro = a.Endereco.Bairro,
        Cidade = a.Endereco.Cidade,
        Uf = a.Endereco.Uf,
    };
}
