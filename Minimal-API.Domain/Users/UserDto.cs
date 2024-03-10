using Minimal_API.Domain.Roles;

namespace Minimal_API.Domain.Users;

public record UserDto(
    Guid Id,
    string Username,
    string Name,
    string LastName,
    string Email,
    string PasswordHash,
    int? PhoneNumber,
    RoleDto Role)
{
}