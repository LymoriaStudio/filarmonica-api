using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IInstrumentoAdminService
{
    Task<AdminInstrumentoDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AdminInstrumentoDto> CreateAsync(CreateInstrumentoRequest request, CancellationToken ct = default);
    Task<AdminInstrumentoDto> UpdateAsync(Guid id, UpdateInstrumentoRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);

    Task<AdminInstrumentoDto> AddFotoAsync(Guid instrumentoId, AddInstrumentoFotoRequest request, CancellationToken ct = default);
    Task<AdminInstrumentoDto> RemoveFotoAsync(Guid instrumentoId, Guid fotoId, CancellationToken ct = default);
}
