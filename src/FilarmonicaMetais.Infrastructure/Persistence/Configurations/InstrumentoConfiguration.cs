using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class InstrumentoConfiguration : IEntityTypeConfiguration<Instrumento>
{
    public void Configure(EntityTypeBuilder<Instrumento> builder)
    {
        builder.ToTable("instrumentos");

        builder.Property(i => i.Slug).HasMaxLength(100).IsRequired();
        builder.Property(i => i.Nome).HasMaxLength(150).IsRequired();
        builder.Property(i => i.Cor).HasMaxLength(20);

        builder.HasIndex(i => i.Slug).IsUnique();

        builder.HasMany(i => i.Galeria)
            .WithOne(f => f.Instrumento)
            .HasForeignKey(f => f.InstrumentoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
