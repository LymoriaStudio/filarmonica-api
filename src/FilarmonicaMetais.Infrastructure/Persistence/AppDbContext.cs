using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FilarmonicaMetais.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<Instrumento> Instrumentos => Set<Instrumento>();
    public DbSet<InstrumentoFoto> InstrumentoFotos => Set<InstrumentoFoto>();
    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Depoimento> Depoimentos => Set<Depoimento>();
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Organizador> Organizadores => Set<Organizador>();
    public DbSet<Interessado> Interessados => Set<Interessado>();
    public DbSet<PedidoApoio> PedidosApoio => Set<PedidoApoio>();
    public DbSet<Doacao> Doacoes => Set<Doacao>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Todo Id é Guid gerado no C# (BaseEntity.Id = Guid.NewGuid()), nunca pelo banco.
        // Sem isso, o provider MySQL (Pomelo) assume por convenção que chaves Guid são
        // geradas pelo banco (ValueGeneratedOnAdd) — o que funciona por acaso quando a
        // entidade é adicionada via Add() explícito (o estado Added é forçado), mas quebra
        // quando ela é adicionada via coleção de navegação de um pai já rastreado (ex:
        // instrumento.Galeria.Add(novaFoto)): o EF, vendo um Id "real" (não default) numa
        // propriedade marcada como gerada pelo banco, conclui que a entidade já existe e
        // emite UPDATE em vez de INSERT — UPDATE que afeta 0 linhas (a foto nunca existiu)
        // e derruba a transação inteira com DbUpdateConcurrencyException. Postgres/SqlServer
        // não têm essa convenção, por isso o bug só apareceu depois da troca pro MySQL.
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var idProperty = entityType.FindProperty("Id");
            if (idProperty is not null && idProperty.ClrType == typeof(Guid))
            {
                idProperty.ValueGenerated = ValueGenerated.Never;
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
