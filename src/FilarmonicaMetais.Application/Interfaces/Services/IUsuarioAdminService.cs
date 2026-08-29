using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.DTOs.Auth;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IUsuarioAdminService
{
    Task<IReadOnlyList<UsuarioDto>> GetAllAsync(CancellationToken ct = default);
    Task<UsuarioDto> CreateAsync(CreateUsuarioRequest request, CancellationToken ct = default);
    Task<UsuarioDto> UpdateAsync(Guid id, UpdateUsuarioRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
