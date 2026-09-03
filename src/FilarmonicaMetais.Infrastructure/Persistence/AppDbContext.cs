using FilarmonicaMetais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        base.OnModelCreating(modelBuilder);
    }
}
