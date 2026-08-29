using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;

namespace FilarmonicaMetais.Application.Services;

public class CursoReadService : ICursoReadService
{
    private readonly IUnitOfWork _uow;
    private readonly IFileStorageService _storage;

    public CursoReadService(IUnitOfWork uow, IFileStorageService storage)
    {
        _uow = uow;
        _storage = storage;
    }

    public async Task<IReadOnlyList<CursoDto>> GetAllAsync(CancellationToken ct = default)
    {
        var cursos = await _uow.Cursos.GetAllComProfessorAsync(ct);
        return cursos.Select(c => new CursoDto
        {
            Id = c.Id,
            ImagemUrl = string.IsNullOrEmpty(c.Imagem) ? null : _storage.ResolvePublicUrl(c.Imagem),
            Nome = c.Nome,
            Descricao = c.Descricao,
            FaixaEtaria = c.FaixaEtaria,
            Duracao = c.Duracao,
            VagasDisponiveis = c.VagasDisponiveis,
            ProfessorId = c.ProfessorId,
            ProfessorNome = c.Professor.Nome,
        }).ToList();
    }
}
