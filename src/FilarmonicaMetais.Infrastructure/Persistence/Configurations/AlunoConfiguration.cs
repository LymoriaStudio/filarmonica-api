using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("alunos");

        builder.Property(a => a.Nome).HasMaxLength(200).IsRequired();
        builder.Property(a => a.Email).HasMaxLength(320);
        builder.Property(a => a.Status).HasConversion<string>().HasMaxLength(20);

        // Owned type: as 7 colunas de endereço continuam soltas na tabela alunos,
        // só o C# fica coeso (STATUS_UI_TO_DB e afins somem — o enum resolve na origem).
        builder.OwnsOne(a => a.Endereco, end =>
        {
            end.Property(e => e.Cep).HasColumnName("zip_code").HasMaxLength(20);
            end.Property(e => e.Logradouro).HasColumnName("street").HasMaxLength(200);
            end.Property(e => e.Numero).HasColumnName("number").HasMaxLength(20);
            end.Property(e => e.Complemento).HasColumnName("complement").HasMaxLength(100);
            end.Property(e => e.Bairro).HasColumnName("neighborhood").HasMaxLength(100);
            end.Property(e => e.Cidade).HasColumnName("city").HasMaxLength(100);
            end.Property(e => e.Uf).HasColumnName("uf").HasMaxLength(2);
        });
    }
}
