using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.DTOs.Common;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IAlunoAdminService
{
    Task<PagedResult<AdminAlunoDto>> BuscarPaginadoAsync(
        string? busca, string? status, int page, int pageSize, CancellationToken ct = default);
    Task<AdminAlunoDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AdminAlunoDto> CreateAsync(CreateAlunoRequest request, CancellationToken ct = default);
    Task<AdminAlunoDto> UpdateAsync(Guid id, UpdateAlunoRequest request, CancellationToken ct = default);
    Task<AdminAlunoDto> UpdateStatusAsync(Guid id, UpdateAlunoStatusRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
