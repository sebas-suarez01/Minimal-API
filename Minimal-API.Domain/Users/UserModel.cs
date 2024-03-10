using Minimal_API.Domain.Permission;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Roles;

namespace Minimal_API.Domain.Users;

public class UserModel : Entity<Guid>
{
    private UserModel()
    {
    }

    private UserModel(Guid id, string username, string name, string lastName, string email, string passwordHash,
        int? phoneNumber)
    {
        this.Id = id;
        this.Username = username;
        this.Name = name;
        this.LastName = lastName;
        this.Email = email;
        this.EmailConfirmed = false;
        this.PasswordHash = passwordHash;
        this.PhoneNumber = phoneNumber;
    }

    public string Username { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public int? PhoneNumber { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public RoleModel Role { get; set; }
    public List<UserPermission> UserPermissions { get; set; }

    public void ConfirmEmail()
    {
        this.EmailConfirmed = true;
    }

    public static UserModel Create(string username, string name, string lastName, string email, string passwordHash,
        int? phoneNumber)
    {
        var id =Guid.NewGuid();
        return new UserModel(id, username, name, lastName, email, passwordHash, phoneNumber);
    }
}