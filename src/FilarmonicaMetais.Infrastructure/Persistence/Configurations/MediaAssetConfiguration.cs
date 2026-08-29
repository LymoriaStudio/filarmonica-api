using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(EntityTypeBuilder<MediaAsset> builder)
    {
        builder.ToTable("media_assets");

        builder.Property(m => m.NomeArquivo).HasMaxLength(255).IsRequired();
        builder.Property(m => m.CaminhoRelativo).IsRequired();
        builder.Property(m => m.Pasta).HasMaxLength(100).IsRequired();
        builder.Property(m => m.ContentType).HasMaxLength(150);

        builder.HasIndex(m => m.CaminhoRelativo).IsUnique();
    }
}
