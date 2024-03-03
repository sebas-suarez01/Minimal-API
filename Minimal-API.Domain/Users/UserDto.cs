using Minimal_API.Domain.Roles;

namespace Minimal_API.Domain.Users;

public class UserDto
{
    public UserId Id { get; private set; }
    public string Username { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public int? PhoneNumber { get; private set; }
    public RoleDto Role { get; private set; }
}