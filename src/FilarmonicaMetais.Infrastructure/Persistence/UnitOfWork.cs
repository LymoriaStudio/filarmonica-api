using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Infrastructure.Persistence.Repositories;

namespace FilarmonicaMetais.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Usuarios = new UsuarioRepository(context);
        Banners = new BannerRepository(context);
        Instrumentos = new InstrumentoRepository(context);
        Eventos = new EventoRepository(context);
        Professores = new ProfessorRepository(context);
        Cursos = new CursoRepository(context);
        Depoimentos = new DepoimentoRepository(context);
        Alunos = new AlunoRepository(context);
        Interessados = new InteressadoRepository(context);
        PedidosApoio = new PedidoApoioRepository(context);
        Doacoes = new DoacaoRepository(context);
        AuditLogs = new AuditLogRepository(context);
        MediaAssets = new MediaAssetRepository(context);
    }

    public IUsuarioRepository Usuarios { get; }
    public IBannerRepository Banners { get; }
    public IInstrumentoRepository Instrumentos { get; }
    public IEventoRepository Eventos { get; }
    public IProfessorRepository Professores { get; }
    public ICursoRepository Cursos { get; }
    public IDepoimentoRepository Depoimentos { get; }
    public IAlunoRepository Alunos { get; }
    public IInteressadoRepository Interessados { get; }
    public IPedidoApoioRepository PedidosApoio { get; }
    public IDoacaoRepository Doacoes { get; }
    public IAuditLogRepository AuditLogs { get; }
    public IMediaAssetRepository MediaAssets { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
}
