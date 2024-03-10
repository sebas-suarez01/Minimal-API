
using Minimal_API.Domain.Users;

namespace Minimal_API.Domain.Permission;

public class UserPermission
{
    public UserPermission()
    {
    }

    public UserPermission(Guid roleId, Guid permissionId)
    {
        UserId = roleId;
        PermissionId = permissionId;
    }

    public Guid UserId { get; set; }
    public UserModel User { get; set; }
    public Guid PermissionId { get; set; }
    public PermissionModel Permission { get; set; }
}