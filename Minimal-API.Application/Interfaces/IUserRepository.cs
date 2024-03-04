using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Interfaces;

public interface IUserRepository : IRepository<UserModel, UserId>
{
    public Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}