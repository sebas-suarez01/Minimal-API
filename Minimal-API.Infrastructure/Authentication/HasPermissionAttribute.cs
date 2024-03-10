using Microsoft.AspNetCore.Authorization;
using Minimal_API.Domain.Enums;

namespace Minimal_API.Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission)
        : base(policy: permission.ToString())
    {
    }
}