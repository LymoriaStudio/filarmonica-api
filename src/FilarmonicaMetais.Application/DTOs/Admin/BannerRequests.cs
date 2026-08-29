namespace FilarmonicaMetais.Application.DTOs.Admin;

// Imagens chegam como caminho relativo já salvo (upload feito antes, via /api/admin/media/upload) —
// o mesmo padrão que o resto da API usa para nunca gravar URL absoluta no banco.
public class CreateBannerRequest
{
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
    public string Status { get; set; } = "Rascunho";
}

public class UpdateBannerRequest : CreateBannerRequest
{
}
