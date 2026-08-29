using FilarmonicaMetais.Application.Common.Exceptions;
using FilarmonicaMetais.Application.DTOs.Admin;
using FilarmonicaMetais.Application.DTOs.Auth;
using FilarmonicaMetais.Application.Interfaces.Repositories;
using FilarmonicaMetais.Application.Interfaces.Services;
using FilarmonicaMetais.Domain.Entities;
using FilarmonicaMetais.Domain.Enums;

namespace FilarmonicaMetais.Application.Services;

public class UsuarioAdminService : IUsuarioAdminService
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _hasher;

    public UsuarioAdminService(IUnitOfWork uow, IPasswordHasher hasher)
    {
        _uow = uow;
        _hasher = hasher;
    }

    public async Task<IReadOnlyList<UsuarioDto>> GetAllAsync(CancellationToken ct = default)
    {
        var usuarios = await _uow.Usuarios.GetAllAsync(ct);
        return usuarios.Select(ToDto).ToList();
    }

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioRequest request, CancellationToken ct = default)
    {
        if (await _uow.Usuarios.GetByEmailAsync(request.Email, ct) is not null)
            throw new ConflictException($"Já existe um usuário com o e-mail '{request.Email}'.");

        var usuario = new Usuario
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _hasher.Hash(request.Password),
            Role = Enum.TryParse<UserRole>(request.Role, true, out var role) ? role : UserRole.Editor,
            JobTitle = request.JobTitle,
            IsActive = true,
        };

        await _uow.Usuarios.AddAsync(usuario, ct);
        await _uow.SaveChangesAsync(ct);

        return ToDto(usuario);
    }

    // E-mail nunca muda por aqui — mesma regra de negócio do PerfilPage no painel
    // (campo bloqueado por padrão, só cargo e nome são editáveis).
    public async Task<UsuarioDto> UpdateAsync(Guid id, UpdateUsuarioRequest request, CancellationToken ct = default)
    {
        var usuario = await _uow.Usuarios.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Usuario), id);

        usuario.FullName = request.FullName;
        usuario.Role = Enum.TryParse<UserRole>(request.Role, true, out var role) ? role : usuario.Role;
        usuario.JobTitle = request.JobTitle;
        usuario.IsActive = request.IsActive;
        usuario.UpdatedAt = DateTime.UtcNow;

        _uow.Usuarios.Update(usuario);
        await _uow.SaveChangesAsync(ct);

        return ToDto(usuario);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var usuario = await _uow.Usuarios.GetByIdAsync(id, ct) ?? throw new NotFoundException(nameof(Usuario), id);
        _uow.Usuarios.Remove(usuario);
        await _uow.SaveChangesAsync(ct);
    }

    private static UsuarioDto ToDto(Usuario u) => new()
    {
        Id = u.Id,
        FullName = u.FullName,
        Email = u.Email,
        Role = u.Role.ToString(),
        JobTitle = u.JobTitle,
        AvatarUrl = u.AvatarUrl,
    };
}
