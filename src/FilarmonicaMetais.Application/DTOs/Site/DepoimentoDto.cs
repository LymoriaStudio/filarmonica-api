namespace FilarmonicaMetais.Application.DTOs.Site;

public class DepoimentoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string TagDetalhe { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
}
