namespace FilarmonicaMetais.Domain.Enums;

// Valores confirmados em RelationshipCMS.tsx: 'novo', 'contacted', 'convertido', 'arquivado'
// (mistura pt-BR/en no dado legado — o enum normaliza para o C#).
public enum InteressadoStatus
{
    Novo = 0,
    Contatado = 1,
    Convertido = 2,
    Arquivado = 3,
}
