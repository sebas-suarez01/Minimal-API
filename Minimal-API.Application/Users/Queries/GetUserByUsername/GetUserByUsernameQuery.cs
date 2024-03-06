using Minimal_API.Application.Abstractions;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Users.Queries.GetUserByUsername;

public record GetUserByUsernameQuery(string Username) : IQuery<UserDto>
{
}