using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class InteressadoConfiguration : IEntityTypeConfiguration<Interessado>
{
    public void Configure(EntityTypeBuilder<Interessado> builder)
    {
        builder.ToTable("interessados");

        builder.Property(i => i.Nome).HasMaxLength(200).IsRequired();
        builder.Property(i => i.Email).HasMaxLength(320).IsRequired();
        builder.Property(i => i.Status).HasConversion<string>().HasMaxLength(20);
    }
}
