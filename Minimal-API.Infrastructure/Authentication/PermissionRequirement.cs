using Microsoft.AspNetCore.Authorization;

namespace Minimal_API.Infrastructure.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Role { get; }
    public string Permission { get; }

    public PermissionRequirement(string role, string permission)
    {
        Role = role;
        Permission = permission;
    }
}