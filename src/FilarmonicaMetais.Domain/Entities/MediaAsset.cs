using FilarmonicaMetais.Domain.Common;

namespace FilarmonicaMetais.Domain.Entities;

// Entidade nova, sem equivalente no Supabase — lá, listAllMedia() lista o bucket
// direto e checkMediaUsage() cruza 4 tabelas comparando URL como string. Disco local
// não dá listagem com metadados de graça, então o arquivo passa a ter registro próprio.
public class MediaAsset : AuditableEntity
{
    public string NomeArquivo { get; set; } = string.Empty;
    public string NomeOriginal { get; set; } = string.Empty;
    public string CaminhoRelativo { get; set; } = string.Empty;
    public string Pasta { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long TamanhoBytes { get; set; }
}
