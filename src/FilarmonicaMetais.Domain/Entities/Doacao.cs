using FilarmonicaMetais.Domain.Common;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Domain.Entities;

public class Doacao : AuditableEntity
{
    public string? NomeDoador { get; set; }
    public TipoDoador TipoDoador { get; set; } = TipoDoador.Fisica;
    public string? CpfCnpj { get; set; }
    public string? EmailDoador { get; set; }
    public decimal Valor { get; set; } // precisão fixada na configuration: HasPrecision(18,2)
    public DateOnly Data { get; set; }
    public DoacaoStatus Status { get; set; } = DoacaoStatus.Pendente;
}
