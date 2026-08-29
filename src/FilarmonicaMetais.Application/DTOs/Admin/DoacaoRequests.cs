namespace FilarmonicaMetais.Application.DTOs.Admin;

public class CreateDoacaoRequest
{
    public string? NomeDoador { get; set; }
    public string TipoDoador { get; set; } = "Fisica";
    public string? CpfCnpj { get; set; }
    public string? EmailDoador { get; set; }
    public decimal Valor { get; set; }
    public string Status { get; set; } = "Pendente";
}

public class UpdateDoacaoRequest : CreateDoacaoRequest
{
}

public class DoacaoAdminDto
{
    public Guid Id { get; set; }
    public string? NomeDoador { get; set; }
    public string TipoDoador { get; set; } = string.Empty;
    public string? CpfCnpj { get; set; }
    public string? EmailDoador { get; set; }
    public decimal Valor { get; set; }
    public DateOnly Data { get; set; }
    public string Status { get; set; } = string.Empty;
}
