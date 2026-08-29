using FilarmonicaMetais.Application.DTOs.Common;
using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class EventoReadService : IEventoReadService
{
    private readonly IUnitOfWork _uow;
    private readonly IFileStorageService _storage;

    public EventoReadService(IUnitOfWork uow, IFileStorageService storage)
    {
        _uow = uow;
        _storage = storage;
    }

    public async Task<PagedResult<EventoDto>> BuscarPaginadoAsync(
        string? categoria, string? status, int page, int pageSize, CancellationToken ct = default)
    {
        EventoStatus? statusEnum = Enum.TryParse<EventoStatus>(status, true, out var parsed) ? parsed : null;

        var (items, totalCount) = await _uow.Eventos.BuscarPaginadoAsync(categoria, statusEnum, page, pageSize, ct);

        return new PagedResult<EventoDto>
        {
            Items = items.Select(ToDto).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }

    public async Task<EventoDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var evento = await _uow.Eventos.GetByIdAsync(id, ct);
        return evento is null ? null : ToDto(evento);
    }

    private EventoDto ToDto(Evento e) => new()
    {
        Id = e.Id,
        ImagemCapaUrl = string.IsNullOrEmpty(e.ImagemCapa) ? null : _storage.ResolvePublicUrl(e.ImagemCapa),
        Titulo = e.Titulo,
        Descricao = e.Descricao,
        Data = e.Data,
        Horario = e.Horario,
        Local = e.Local,
        Endereco = e.Endereco,
        GoogleMapsUrl = e.GoogleMapsUrl,
        Categoria = e.Categoria,
        Status = e.Status.ToString(),
        Destaque = e.Destaque,
        Link = e.Link,
        Pago = e.Pago,
        ValorIngresso = e.ValorIngresso,
    };
}
