namespace FilarmonicaMetais.Infrastructure.FileStorage;

public class LocalFileStorageOptions
{
    public const string SectionName = "FileStorage";

    // Pasta física no disco (volume do Railway) onde os arquivos são gravados.
    public string BasePath { get; set; } = "uploads";

    // Host público usado para compor a URL a partir do caminho relativo salvo no banco —
    // trocar de host (Railway → servidor do cliente) é só mudar esta variável de ambiente,
    // nenhuma linha do banco precisa ser tocada. Ver decisão de projeto no plano de migração.
    public string PublicBaseUrl { get; set; } = string.Empty;
}
