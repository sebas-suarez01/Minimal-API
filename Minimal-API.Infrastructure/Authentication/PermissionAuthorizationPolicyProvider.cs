using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Minimal_API.Infrastructure.Authentication;

public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        var values = policyName.Split(' ');

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(values[0], values[1]))
            .Build();
    }
}