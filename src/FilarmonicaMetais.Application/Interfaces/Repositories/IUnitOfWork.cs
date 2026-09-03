namespace FilarmonicaMetais.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUsuarioRepository Usuarios { get; }
    IBannerRepository Banners { get; }
    IInstrumentoRepository Instrumentos { get; }
    IEventoRepository Eventos { get; }
    IProfessorRepository Professores { get; }
    ICursoRepository Cursos { get; }
    IDepoimentoRepository Depoimentos { get; }
    IAlunoRepository Alunos { get; }
    IOrganizadorRepository Organizadores { get; }
    IInteressadoRepository Interessados { get; }
    IPedidoApoioRepository PedidosApoio { get; }
    IDoacaoRepository Doacoes { get; }
    IAuditLogRepository AuditLogs { get; }
    IMediaAssetRepository MediaAssets { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
