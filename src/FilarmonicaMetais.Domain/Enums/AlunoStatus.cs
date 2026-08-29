namespace FilarmonicaMetais.Domain.Enums;

// Espelha STATUS_DB_TO_UI / STATUS_UI_TO_DB de studentsService.ts —
// o mapa manual de string some, o enum passa a ser a única fonte de verdade.
public enum AlunoStatus
{
    Ativo = 0,
    Inativo = 1,
    Formado = 2,
    Arquivado = 3,
}
