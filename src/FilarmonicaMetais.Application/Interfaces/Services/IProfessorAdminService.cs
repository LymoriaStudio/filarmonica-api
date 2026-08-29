using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IProfessorAdminService
{
    Task<IReadOnlyList<AdminProfessorDto>> GetAllAsync(CancellationToken ct = default);
    Task<AdminProfessorDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AdminProfessorDto> CreateAsync(CreateProfessorRequest request, CancellationToken ct = default);
    Task<AdminProfessorDto> UpdateAsync(Guid id, UpdateProfessorRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
