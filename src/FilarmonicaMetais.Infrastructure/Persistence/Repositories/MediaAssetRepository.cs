using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Domain.Entities;

namespace FilarmonicaMetais.Infrastructure.Persistence.Repositories;

public class MediaAssetRepository : GenericRepository<MediaAsset>, IMediaAssetRepository
{
    public MediaAssetRepository(AppDbContext context) : base(context) { }
}
