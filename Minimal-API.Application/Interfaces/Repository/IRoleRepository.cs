using Minimal_API.Domain.Roles;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Interfaces.Repository;

public interface IRoleRepository
{
    public Task<Result<RoleModel>> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result<RoleModel>> GetRoleByNameAsync(string name, CancellationToken cancellationToken = default);
}