using FilarmonicaMetais.Domain.Common;

namespace FilarmonicaMetais.Domain.Entities;

// Shape confirmado contra a interface Testimonial do front:
// { id: number, name, tag, tag_detail, text, order }.
// Id decidido como Guid (BaseEntity), alinhado às outras 12 entidades —
// nenhuma FK aponta para esta tabela, então a troca de tipo não tem raio de impacto.
public class Depoimento : AuditableEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string TagDetalhe { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
