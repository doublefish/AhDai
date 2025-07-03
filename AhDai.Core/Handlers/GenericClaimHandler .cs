using AhDai.Core.Attributes;
using AhDai.Core.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Core.Handlers;

/// <summary>
/// GenericClaimHandler
/// </summary>
public class GenericClaimHandler : AuthorizationHandler<ClaimRequirement>
{
    /// <summary>
    /// HandleRequirementAsync
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement"></param>
    /// <returns></returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            return Task.CompletedTask;
        }
        if (context.Resource is not HttpContext httpContext)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var bypassAttribute = httpContext.GetEndpoint()?.Metadata.GetMetadata<AllowClaimBypassAttribute>();
        if (bypassAttribute != null)
        {
            if (bypassAttribute.ClaimTypes == null || bypassAttribute.ClaimTypes.Length == 0 || bypassAttribute.ClaimTypes.Contains(requirement.ClaimType))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        if (context.User.HasClaim(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail(new AuthorizationFailureReason(this, requirement.Message));
        }
        return Task.CompletedTask;
    }
}
