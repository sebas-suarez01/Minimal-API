using Microsoft.AspNetCore.Authorization;
using Minimal_API.Domain.Enums;

namespace Minimal_API.Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string role, Permission permission)
        : base(policy: string.Join(separator:' ',role, permission.ToString()))
    {
    }
}