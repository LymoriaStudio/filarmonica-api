using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class EventoConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        builder.ToTable("eventos");

        builder.Property(e => e.Titulo).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Categoria).HasMaxLength(100);
        builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);

        // Precisão fixada (regra #3) — sem isso o default varia por provider
        // e trunca dinheiro silenciosamente.
        builder.Property(e => e.ValorIngresso).HasPrecision(18, 2);
    }
}
