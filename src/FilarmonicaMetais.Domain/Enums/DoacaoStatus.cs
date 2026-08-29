namespace FilarmonicaMetais.Domain.Enums;

// Valores confirmados: 'confirmado' e 'pendente' — usados no select adicionado
// ao drawer de edição de doações (FinanceiroERP.tsx).
public enum DoacaoStatus
{
    Pendente = 0,
    Confirmado = 1,
}
