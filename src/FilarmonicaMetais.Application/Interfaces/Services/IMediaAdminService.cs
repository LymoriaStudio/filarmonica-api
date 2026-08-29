using FilarmonicaMetais.Application.DTOs.Admin;

namespace FilarmonicaMetais.Application.Interfaces.Services;

public interface IMediaAdminService
{
    Task<MediaAssetDto> UploadAsync(Stream conteudo, string nomeOriginal, string contentType, string pasta, CancellationToken ct = default);
    Task<IReadOnlyList<MediaAssetDto>> GetAllAsync(CancellationToken ct = default);
    Task<MediaUsoDto> CheckUsoAsync(Guid id, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
