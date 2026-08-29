using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IBannerAdminService
{
    Task<IReadOnlyList<AdminBannerDto>> GetAllAsync(CancellationToken ct = default);
    Task<AdminBannerDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AdminBannerDto> CreateAsync(CreateBannerRequest request, CancellationToken ct = default);
    Task<AdminBannerDto> UpdateAsync(Guid id, UpdateBannerRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
