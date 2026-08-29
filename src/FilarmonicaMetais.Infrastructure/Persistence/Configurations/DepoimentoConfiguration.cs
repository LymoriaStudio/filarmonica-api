using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class DepoimentoConfiguration : IEntityTypeConfiguration<Depoimento>
{
    public void Configure(EntityTypeBuilder<Depoimento> builder)
    {
        builder.ToTable("depoimentos");

        builder.Property(d => d.Nome).HasMaxLength(200).IsRequired();
        builder.Property(d => d.Tag).HasMaxLength(100);
        builder.Property(d => d.TagDetalhe).HasMaxLength(200);
        builder.Property(d => d.Texto).IsRequired();
        builder.Property(d => d.DisplayOrder).HasColumnName("display_order");
    }
}
