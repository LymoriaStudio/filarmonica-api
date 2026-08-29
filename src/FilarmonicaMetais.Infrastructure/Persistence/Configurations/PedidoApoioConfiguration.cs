using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class PedidoApoioConfiguration : IEntityTypeConfiguration<PedidoApoio>
{
    public void Configure(EntityTypeBuilder<PedidoApoio> builder)
    {
        builder.ToTable("pedidos_apoio");

        builder.Property(p => p.Nome).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(320).IsRequired();
        builder.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);
    }
}
