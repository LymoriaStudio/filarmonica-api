using FilarmonicaMetais.Domain.Common;

namespace FilarmonicaMetais.Domain.Entities;

public class Instrumento : AuditableEntity
{
    public string Slug { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string DescricaoLonga { get; set; } = string.Empty;
    public string Imagem { get; set; } = string.Empty; // caminho relativo
    public string? VideoUrl { get; set; }
    public string Cor { get; set; } = "#001856";

    public ICollection<InstrumentoFoto> Galeria { get; set; } = new List<InstrumentoFoto>();
}
