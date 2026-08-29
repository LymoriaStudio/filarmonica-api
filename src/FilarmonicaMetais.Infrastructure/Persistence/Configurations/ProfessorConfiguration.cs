using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.ToTable("professores");

        builder.Property(p => p.Nome).HasMaxLength(200).IsRequired();
        builder.Property(p => p.DisplayOrder).HasColumnName("display_order");

        // Owned type: as 5 colunas social_* continuam na mesma tabela (professores),
        // só o C# fica coeso. Nenhuma tabela nova.
        builder.OwnsOne(p => p.Redes, redes =>
        {
            redes.Property(r => r.Instagram).HasColumnName("social_instagram");
            redes.Property(r => r.Facebook).HasColumnName("social_facebook");
            redes.Property(r => r.Youtube).HasColumnName("social_youtube");
            redes.Property(r => r.Linkedin).HasColumnName("social_linkedin");
            redes.Property(r => r.Whatsapp).HasColumnName("social_whatsapp");
        });
    }
}
