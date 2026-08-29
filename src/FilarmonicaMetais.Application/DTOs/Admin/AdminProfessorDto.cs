namespace FilarmonicaMetais.Application.DTOs.Admin;

public class AdminProfessorDto
{
    public Guid Id { get; set; }
    public string? Foto { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string? Especialidade { get; set; }
    public string Instrumento { get; set; } = string.Empty;
    public string MiniBio { get; set; } = string.Empty;
    public string BioCompleta { get; set; } = string.Empty;
    public string? Instagram { get; set; }
    public string? Facebook { get; set; }
    public string? Youtube { get; set; }
    public string? Linkedin { get; set; }
    public string? Whatsapp { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public bool Destaque { get; set; }
    public int DisplayOrder { get; set; }
}
