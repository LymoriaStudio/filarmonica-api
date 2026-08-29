using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class PedidoApoioRepository : GenericRepository<PedidoApoio>, IPedidoApoioRepository
{
    public PedidoApoioRepository(AppDbContext context) : base(context) { }
}
