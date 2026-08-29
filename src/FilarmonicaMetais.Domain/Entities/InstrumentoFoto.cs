using FilarmonicaMetais.Domain.Common;

namespace FilarmonicaMetais.Domain.Entities;

// Normalização do array `gallery text[]` (instruments.gallery no Supabase),
// que não existe no SQL Server. handleRemoveGalleryImage(url) confirma que
// hoje a galeria é só lista de URLs, sem legenda — a ordem vira coluna própria.
public class InstrumentoFoto : BaseEntity
{
    public Guid InstrumentoId { get; set; }
    public Instrumento Instrumento { get; set; } = null!;
    public string Url { get; set; } = string.Empty; // caminho relativo
    public int Ordem { get; set; }
}
