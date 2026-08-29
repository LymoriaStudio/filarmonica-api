using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class BannerConfiguration : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.ToTable("banners");

        builder.Property(b => b.Title).HasMaxLength(200).IsRequired();
        builder.Property(b => b.Status).HasConversion<string>().HasMaxLength(20);

        // "order" é palavra reservada em SQL — nome de coluna explícito (regra #2)
        // evita colisão em qualquer script manual, mesmo o EF já escapando por padrão.
        builder.Property(b => b.DisplayOrder).HasColumnName("display_order");
    }
}
