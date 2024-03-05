namespace Minimal_API.Domain.Roles.Shared;

public static class RoleMapping
{
    public const string USER = "User";
    public const string ADMIN = "Admin";
    public const string SUPERADMIN = "SuperAdmin";
    
    public static string MapRole(RoleEnum role)
    {
        return role switch
        {
            RoleEnum.User => USER,
            RoleEnum.Admin => ADMIN,
            RoleEnum.SuperAdmin => SUPERADMIN
        };
    }
}