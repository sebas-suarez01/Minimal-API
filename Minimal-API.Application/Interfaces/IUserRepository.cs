using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Interfaces;

public interface IUserRepository : IRepository<UserModel, UserId, UserDto>
{
    
}