namespace FilarmonicaMetais.Application.DTOs.Site;

public class BannerDto
{
    public Guid Id { get; set; }
    public string ImageDesktopUrl { get; set; } = string.Empty;
    public string ImageMobileUrl { get; set; } = string.Empty;
    public string? Tag { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Text { get; set; }
    public string? PrimaryBtnText { get; set; }
    public string? PrimaryBtnLink { get; set; }
    public string? SecondaryBtnText { get; set; }
    public string? SecondaryBtnLink { get; set; }
    public int DisplayOrder { get; set; }
}
