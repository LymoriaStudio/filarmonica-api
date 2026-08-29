namespace FilarmonicaMetais.Application.DTOs.Admin;

public class CreateDepoimentoRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string TagDetalhe { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}

public class UpdateDepoimentoRequest : CreateDepoimentoRequest
{
}
