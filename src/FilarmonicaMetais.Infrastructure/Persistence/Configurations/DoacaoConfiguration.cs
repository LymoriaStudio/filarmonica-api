using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class DoacaoConfiguration : IEntityTypeConfiguration<Doacao>
{
    public void Configure(EntityTypeBuilder<Doacao> builder)
    {
        builder.ToTable("doacoes");

        builder.Property(d => d.TipoDoador).HasConversion<string>().HasMaxLength(20);
        builder.Property(d => d.Status).HasConversion<string>().HasMaxLength(20);

        // Precisão fixada (regra #3) — dinheiro nunca fica ao sabor do default do provider.
        builder.Property(d => d.Valor).HasPrecision(18, 2);
    }
}
