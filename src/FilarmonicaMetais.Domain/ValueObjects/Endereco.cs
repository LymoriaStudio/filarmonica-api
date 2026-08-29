namespace FilarmonicaMetais.Domain.ValueObjects;

// Owned type — mesmas 7 colunas que hoje vivem soltas em students
// (zip_code, street, number, complement, neighborhood, city, uf),
// agrupadas aqui só para o C# ficar coeso. Nenhuma tabela nova.
public class Endereco
{
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Uf { get; set; }
}
