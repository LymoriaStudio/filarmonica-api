using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class EventoAdminService : IEventoAdminService
{
    private readonly IUnitOfWork _uow;

    public EventoAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<AdminEventoDto>> GetAllAsync(CancellationToken ct = default)
    {
        var eventos = await _uow.Eventos.GetAllAsync(ct);
        return eventos.OrderByDescending(e => e.Data).Select(ToDto).ToList();
    }

    public async Task<AdminEventoDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var evento = await _uow.Eventos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Evento), id);
        return ToDto(evento);
    }

    public async Task<AdminEventoDto> CreateAsync(CreateEventoRequest request, CancellationToken ct = default)
    {
        var evento = new Evento();
        Apply(evento, request);

        await _uow.Eventos.AddAsync(evento, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(evento);
    }

    public async Task<AdminEventoDto> UpdateAsync(Guid id, UpdateEventoRequest request, CancellationToken ct = default)
    {
        var evento = await _uow.Eventos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Evento), id);

        Apply(evento, request);
        evento.UpdatedAt = DateTime.UtcNow;

        _uow.Eventos.Update(evento);
        await _uow.SaveChangesAsync(ct);

        return ToDto(evento);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var evento = await _uow.Eventos.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Evento), id);
        _uow.Eventos.Remove(evento);
        await _uow.SaveChangesAsync(ct);
    }

    private static void Apply(Evento evento, CreateEventoRequest request)
    {
        evento.ImagemCapa = string.IsNullOrWhiteSpace(request.ImagemCapa) ? null : request.ImagemCapa;
        evento.Titulo = request.Titulo;
        evento.Descricao = request.Descricao;
        evento.Data = request.Data;
        evento.Horario = request.Horario;
        evento.Local = request.Local;
        evento.Endereco = request.Endereco;
        evento.GoogleMapsUrl = request.GoogleMapsUrl;
        evento.Categoria = request.Categoria;
        evento.Status = Enum.TryParse<EventoStatus>(request.Status, true, out var status) ? status : EventoStatus.Rascunho;
        evento.Destaque = request.Destaque;
        evento.Link = request.Link;
        evento.Pago = request.Pago;
        evento.ValorIngresso = request.ValorIngresso;
    }

    private static AdminEventoDto ToDto(Evento e) => new()
    {
        Id = e.Id,
        ImagemCapa = e.ImagemCapa,
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
