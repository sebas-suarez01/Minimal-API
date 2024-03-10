using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Users;

namespace Minimal_API.Domain.Permission;

public class PermissionModel : Entity<Guid>
{
    private PermissionModel()
    {}
    private PermissionModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public string Name { get; private set; }
    public List<UserPermission> UserPermissions { get; set; }

    public static PermissionModel Create(string name)
    {
        var id = Guid.NewGuid();
        return new PermissionModel(id, name);
    }
    public static PermissionModel Create(Guid id, string name)
    {
        var role = new PermissionModel(id, name)
        {
            CreatedUtc = new DateTime(2024, 3, 7, 12, 0, 0 , DateTimeKind.Utc)
        };
        return role;
    }
}