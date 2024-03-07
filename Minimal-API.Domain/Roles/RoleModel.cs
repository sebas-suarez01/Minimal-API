using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Users;

namespace Minimal_API.Domain.Roles;

public class RoleModel : Entity<RoleId>
{
    private RoleModel()
    {
    }

    private RoleModel(RoleId id, string name)
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
        var id = ValueObjectId.CreateUnique<RoleId>();
        return new RoleModel(id, name);
    }
    public static RoleModel Create(Guid id, string name)
    {
        var idInstance = ValueObjectId.Create<RoleId>(id);
        var role = new RoleModel(idInstance, name)
        {
            CreatedUtc = new DateTime(2024, 3, 7, 12, 0, 0 , DateTimeKind.Utc)
        };
        return role;
    }
}