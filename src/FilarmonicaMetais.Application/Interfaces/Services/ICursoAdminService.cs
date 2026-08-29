using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface ICursoAdminService
{
    Task<IReadOnlyList<AdminCursoDto>> GetAllAsync(CancellationToken ct = default);
    Task<AdminCursoDto> CreateAsync(CreateCursoRequest request, CancellationToken ct = default);
    Task<AdminCursoDto> UpdateAsync(Guid id, UpdateCursoRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
