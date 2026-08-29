using FilarmonicaMetais.Application.DTOs.Site;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IProfessorReadService
{
    Task<IReadOnlyList<ProfessorDto>> GetAllAsync(CancellationToken ct = default);
}
