using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class DoacaoRepository : GenericRepository<Doacao>, IDoacaoRepository
{
    public DoacaoRepository(AppDbContext context) : base(context) { }
}
