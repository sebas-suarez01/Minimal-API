using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Minimal_API.Infrastructure.Authentication.Jwt;

namespace Minimal_API.Infrastructure.Authentication;

public class PermissionAuthorizationHandler :AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context
            .User
            .Claims
            .Where(x => x.Type == CustomClaims.Permissions)
            .Select(x => x.Value)
            .ToHashSet();
        var role = context
            .User
            .Claims
            .Where(x => x.Type == ClaimTypes.Role)
            .Select(x => x.Value)
            .FirstOrDefault();

        if (role is not null && role == requirement.Role && permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}