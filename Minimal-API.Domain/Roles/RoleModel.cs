using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Users;

namespace Minimal_API.Domain.Roles;

public class RoleModel : Entity<Guid>
{
    private RoleModel()
    {
    }

    private RoleModel(Guid id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.NormalizedName = name.ToUpper();
    }

    public string Name { get; init; }
    public string NormalizedName { get; init; }
    public List<UserModel> Users { get; private set; }

    public static RoleModel Create(string name)
    {
        var id = Guid.NewGuid();
        return new RoleModel(id, name);
    }
    public static RoleModel Create(Guid id, string name)
    {;
        var role = new RoleModel(id, name)
        {
            CreatedUtc = new DateTime(2024, 3, 7, 12, 0, 0 , DateTimeKind.Utc)
        };
        return role;
    }
}