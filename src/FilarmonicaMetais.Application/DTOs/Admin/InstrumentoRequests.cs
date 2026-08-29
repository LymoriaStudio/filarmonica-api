namespace FilarmonicaMetais.Application.DTOs.Admin;

public class CreateInstrumentoRequest
{
    public string Slug { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string DescricaoLonga { get; set; } = string.Empty;
    public string Imagem { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string Cor { get; set; } = "#001856";
}

public class UpdateInstrumentoRequest : CreateInstrumentoRequest
{
}

public class AddInstrumentoFotoRequest
{
    public string Url { get; set; } = string.Empty;
}
