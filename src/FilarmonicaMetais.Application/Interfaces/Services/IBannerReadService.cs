using FilarmonicaMetais.Application.DTOs.Site;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IBannerReadService
{
    Task<IReadOnlyList<BannerDto>> GetAtivosAsync(CancellationToken ct = default);
}
