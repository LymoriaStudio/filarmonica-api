namespace FilarmonicaMetais.Application.DTOs.Admin;

// Diferente do BannerDto público: expõe o caminho RELATIVO cru (não a URL resolvida) —
// é o que o formulário de edição do painel precisa reenviar se a imagem não mudar.
public class AdminBannerDto
{
    public Guid Id { get; set; }
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
    public string Status { get; set; } = string.Empty;
}
