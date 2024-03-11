using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Interfaces;

public interface IUserRepository : IRepository<UserModel, Guid>
{
    public Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result<UserDto>> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    public Task<Result<UserDto>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task<Result> ChangeRoleAsync(Guid id, string roleName, CancellationToken cancellationToken = default);
    public Task<Result<List<string>>> GetPermissionsAsync(Guid id, CancellationToken cancellationToken = default);
}