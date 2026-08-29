using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;

namespace FilarmonicaMetais.Application.Services;

public class ProfessorReadService : IProfessorReadService
{
    private readonly IUnitOfWork _uow;
    private readonly IFileStorageService _storage;

    public ProfessorReadService(IUnitOfWork uow, IFileStorageService storage)
    {
        _uow = uow;
        _storage = storage;
    }

    public async Task<IReadOnlyList<ProfessorDto>> GetAllAsync(CancellationToken ct = default)
    {
        var professores = await _uow.Professores.GetOrdenadosAsync(ct);
        return professores.Select(p => new ProfessorDto
        {
            Id = p.Id,
            FotoUrl = string.IsNullOrEmpty(p.Foto) ? null : _storage.ResolvePublicUrl(p.Foto),
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
        }).ToList();
    }
}
