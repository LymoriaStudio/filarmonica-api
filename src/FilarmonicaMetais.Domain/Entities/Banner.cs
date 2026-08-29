using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Domain.Entities;

public class Banner : AuditableEntity
{
    // Caminho relativo no storage — não URL absoluta. Ver decisão de projeto no plano de migração.
    public string ImageDesktop { get; set; } = string.Empty;
    public string ImageMobile { get; set; } = string.Empty;
    public string? Tag { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Text { get; set; }
    public string? PrimaryBtnText { get; set; }
    public string? PrimaryBtnLink { get; set; }
    public string? SecondaryBtnText { get; set; }
    public string? SecondaryBtnLink { get; set; }
    public int DisplayOrder { get; set; }
    public BannerStatus Status { get; set; } = BannerStatus.Rascunho;
}
