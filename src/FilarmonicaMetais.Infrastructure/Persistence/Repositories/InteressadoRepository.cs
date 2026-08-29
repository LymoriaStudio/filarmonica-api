using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class InteressadoRepository : GenericRepository<Interessado>, IInteressadoRepository
{
    public InteressadoRepository(AppDbContext context) : base(context) { }
}
