using FilarmonicaMetais.Application.DTOs.Site;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;

namespace FilarmonicaMetais.Application.Services;

public class BannerReadService : IBannerReadService
{
    private readonly IUnitOfWork _uow;
    private readonly IFileStorageService _storage;

    public BannerReadService(IUnitOfWork uow, IFileStorageService storage)
    {
        _uow = uow;
        _storage = storage;
    }

    public async Task<IReadOnlyList<BannerDto>> GetAtivosAsync(CancellationToken ct = default)
    {
        var banners = await _uow.Banners.GetAtivosOrdenadosAsync(ct);
        return banners.Select(b => new BannerDto
        {
            Id = b.Id,
            ImageDesktopUrl = _storage.ResolvePublicUrl(b.ImageDesktop),
            ImageMobileUrl = _storage.ResolvePublicUrl(b.ImageMobile),
            Tag = b.Tag,
            Title = b.Title,
            Subtitle = b.Subtitle,
            Text = b.Text,
            PrimaryBtnText = b.PrimaryBtnText,
            PrimaryBtnLink = b.PrimaryBtnLink,
            SecondaryBtnText = b.SecondaryBtnText,
            SecondaryBtnLink = b.SecondaryBtnLink,
            DisplayOrder = b.DisplayOrder,
        }).ToList();
    }
}
