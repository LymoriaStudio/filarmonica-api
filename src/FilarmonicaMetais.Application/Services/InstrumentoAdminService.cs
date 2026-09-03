using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Application.Services;

public class InstrumentoAdminService : IInstrumentoAdminService
{
    private readonly IUnitOfWork _uow;

    public InstrumentoAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<AdminInstrumentoDto>> GetAllAsync(CancellationToken ct = default)
    {
        // GetAllAsync do repositório genérico já inclui a galeria (InstrumentoRepository
        // sobrescreve isso) — mesma listagem que a leitura pública usa.
        var instrumentos = await _uow.Instrumentos.GetAllAsync(ct);
        return instrumentos.Select(ToDto).ToList();
    }

    public async Task<AdminInstrumentoDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var instrumento = await _uow.Instrumentos.GetByIdComGaleriaAsync(id, ct)
            ?? throw new NotFoundException(nameof(Instrumento), id);
        return ToDto(instrumento);
    }

    public async Task<AdminInstrumentoDto> CreateAsync(CreateInstrumentoRequest request, CancellationToken ct = default)
    {
        var instrumento = new Instrumento();
        Apply(instrumento, request);

        await _uow.Instrumentos.AddAsync(instrumento, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(instrumento);
    }

    public async Task<AdminInstrumentoDto> UpdateAsync(Guid id, UpdateInstrumentoRequest request, CancellationToken ct = default)
    {
        var instrumento = await _uow.Instrumentos.GetByIdComGaleriaAsync(id, ct)
            ?? throw new NotFoundException(nameof(Instrumento), id);

        Apply(instrumento, request);
        instrumento.UpdatedAt = DateTime.UtcNow;

        _uow.Instrumentos.Update(instrumento);
        await _uow.SaveChangesAsync(ct);

        return ToDto(instrumento);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var instrumento = await _uow.Instrumentos.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Instrumento), id);
        _uow.Instrumentos.Remove(instrumento);
        await _uow.SaveChangesAsync(ct);
    }

    // handleRemoveGalleryImage(url) no front filtrava só por string de URL — aqui a galeria
    // é tabela própria (InstrumentoFoto), então adicionar é só o próximo número de ordem.
    public async Task<AdminInstrumentoDto> AddFotoAsync(Guid instrumentoId, AddInstrumentoFotoRequest request, CancellationToken ct = default)
    {
        var instrumento = await _uow.Instrumentos.GetByIdComGaleriaAsync(instrumentoId, ct)
            ?? throw new NotFoundException(nameof(Instrumento), instrumentoId);

        var proximaOrdem = instrumento.Galeria.Count == 0 ? 0 : instrumento.Galeria.Max(f => f.Ordem) + 1;
        instrumento.Galeria.Add(new InstrumentoFoto
        {
            InstrumentoId = instrumentoId,
            Url = request.Url,
            Ordem = proximaOrdem,
        });

        await _uow.SaveChangesAsync(ct);
        return ToDto(instrumento);
    }

    public async Task<AdminInstrumentoDto> RemoveFotoAsync(Guid instrumentoId, Guid fotoId, CancellationToken ct = default)
    {
        var instrumento = await _uow.Instrumentos.GetByIdComGaleriaAsync(instrumentoId, ct)
            ?? throw new NotFoundException(nameof(Instrumento), instrumentoId);

        var foto = instrumento.Galeria.FirstOrDefault(f => f.Id == fotoId)
            ?? throw new NotFoundException(nameof(InstrumentoFoto), fotoId);

        instrumento.Galeria.Remove(foto);
        await _uow.SaveChangesAsync(ct);

        return ToDto(instrumento);
    }

    private static void Apply(Instrumento instrumento, CreateInstrumentoRequest request)
    {
        instrumento.Slug = request.Slug;
        instrumento.Nome = request.Nome;
        instrumento.Descricao = request.Descricao;
        instrumento.DescricaoLonga = request.DescricaoLonga;
        instrumento.Imagem = request.Imagem;
        instrumento.VideoUrl = request.VideoUrl;
        instrumento.Cor = request.Cor;
    }

    private static AdminInstrumentoDto ToDto(Instrumento i) => new()
    {
        Id = i.Id,
        Slug = i.Slug,
        Nome = i.Nome,
        Descricao = i.Descricao,
        DescricaoLonga = i.DescricaoLonga,
        Imagem = i.Imagem,
        VideoUrl = i.VideoUrl,
        Cor = i.Cor,
        Galeria = i.Galeria.OrderBy(f => f.Ordem)
            .Select(f => new AdminInstrumentoFotoDto { Id = f.Id, Url = f.Url, Ordem = f.Ordem })
            .ToList(),
    };
}
