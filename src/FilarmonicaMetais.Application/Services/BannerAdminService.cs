using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class BannerAdminService : IBannerAdminService
{
    private readonly IUnitOfWork _uow;

    public BannerAdminService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<AdminBannerDto>> GetAllAsync(CancellationToken ct = default)
    {
        var banners = await _uow.Banners.GetAllAsync(ct);
        return banners.OrderBy(b => b.DisplayOrder).Select(ToDto).ToList();
    }

    public async Task<AdminBannerDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var banner = await _uow.Banners.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Banner), id);
        return ToDto(banner);
    }

    public async Task<AdminBannerDto> CreateAsync(CreateBannerRequest request, CancellationToken ct = default)
    {
        var banner = new Banner
        {
            ImageDesktop = request.ImageDesktop,
            ImageMobile = request.ImageMobile,
            Tag = request.Tag,
            Title = request.Title,
            Subtitle = request.Subtitle,
            Text = request.Text,
            PrimaryBtnText = request.PrimaryBtnText,
            PrimaryBtnLink = request.PrimaryBtnLink,
            SecondaryBtnText = request.SecondaryBtnText,
            SecondaryBtnLink = request.SecondaryBtnLink,
            DisplayOrder = request.DisplayOrder,
            Status = ParseStatus(request.Status),
        };

        await ShiftDisplayOrderAsync(banner.DisplayOrder, excludeId: null, ct);
        await _uow.Banners.AddAsync(banner, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(banner);
    }

    public async Task<AdminBannerDto> UpdateAsync(Guid id, UpdateBannerRequest request, CancellationToken ct = default)
    {
        var banner = await _uow.Banners.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Banner), id);

        await ShiftDisplayOrderAsync(request.DisplayOrder, excludeId: id, ct);

        banner.ImageDesktop = request.ImageDesktop;
        banner.ImageMobile = request.ImageMobile;
        banner.Tag = request.Tag;
        banner.Title = request.Title;
        banner.Subtitle = request.Subtitle;
        banner.Text = request.Text;
        banner.PrimaryBtnText = request.PrimaryBtnText;
        banner.PrimaryBtnLink = request.PrimaryBtnLink;
        banner.SecondaryBtnText = request.SecondaryBtnText;
        banner.SecondaryBtnLink = request.SecondaryBtnLink;
        banner.DisplayOrder = request.DisplayOrder;
        banner.Status = ParseStatus(request.Status);
        banner.UpdatedAt = DateTime.UtcNow;

        _uow.Banners.Update(banner);
        await _uow.SaveChangesAsync(ct);

        return ToDto(banner);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var banner = await _uow.Banners.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Banner), id);

        _uow.Banners.Remove(banner);
        await _uow.SaveChangesAsync(ct);
    }

    // Porta a regra de reordenação implementada no painel React (SiteCMS.tsx):
    // ao salvar um banner numa ordem já ocupada, empurra em cadeia os banners
    // subsequentes +1, em vez de simplesmente colidir com o que já existia.
    private async Task ShiftDisplayOrderAsync(int newOrder, Guid? excludeId, CancellationToken ct)
    {
        var todos = await _uow.Banners.GetAllAsync(ct);
        var conflitantes = todos
            .Where(b => b.Id != excludeId && b.DisplayOrder >= newOrder)
            .OrderBy(b => b.DisplayOrder)
            .ToList();

        var esperado = newOrder + 1;
        foreach (var banner in conflitantes)
        {
            if (banner.DisplayOrder < esperado)
            {
                banner.DisplayOrder = esperado;
                _uow.Banners.Update(banner);
                esperado++;
            }
            else
            {
                break; // lacuna encontrada — cadeia de colisão termina aqui
            }
        }
    }

    private static BannerStatus ParseStatus(string status) =>
        Enum.TryParse<BannerStatus>(status, true, out var parsed) ? parsed : BannerStatus.Rascunho;

    private static AdminBannerDto ToDto(Banner b) => new()
    {
        Id = b.Id,
        ImageDesktop = b.ImageDesktop,
        ImageMobile = b.ImageMobile,
        Tag = b.Tag,
        Title = b.Title,
        Subtitle = b.Subtitle,
        Text = b.Text,
        PrimaryBtnText = b.PrimaryBtnText,
        PrimaryBtnLink = b.PrimaryBtnLink,
        SecondaryBtnText = b.SecondaryBtnText,
        SecondaryBtnLink = b.SecondaryBtnLink,
        DisplayOrder = b.DisplayOrder,
        Status = b.Status.ToString(),
    };
}
