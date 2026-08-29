using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilarmonicaMetais.Infrastructure.Persistence.Configurations;

public class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("cursos");

        builder.Property(c => c.Nome).HasMaxLength(200).IsRequired();

        // FK consolidada (decisão de migração) — sem coluna de texto duplicada
        // para o nome do professor. Restrict: não deixa apagar um professor
        // com curso vinculado, para não perder a referência do curso.
        builder.HasOne(c => c.Professor)
            .WithMany(p => p.Cursos)
            .HasForeignKey(c => c.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
