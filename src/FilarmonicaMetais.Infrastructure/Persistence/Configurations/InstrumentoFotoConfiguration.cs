using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

// Normalização da coluna `gallery text[]` do Supabase — arrays não existem no SQL Server.
public class InstrumentoFotoConfiguration : IEntityTypeConfiguration<InstrumentoFoto>
{
    public void Configure(EntityTypeBuilder<InstrumentoFoto> builder)
    {
        builder.ToTable("instrumento_fotos");

        builder.Property(f => f.Url).IsRequired();
        builder.Property(f => f.Ordem).HasColumnName("ordem");
    }
}
