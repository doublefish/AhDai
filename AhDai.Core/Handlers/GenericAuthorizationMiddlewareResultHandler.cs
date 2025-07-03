using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Core.Handlers;

/// <summary>
/// GenericAuthorizationMiddlewareResultHandler
/// </summary>
public class GenericAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    /// <summary>
    /// HandleAsync
    /// </summary>
    /// <param name="next"></param>
    /// <param name="context"></param>
    /// <param name="policy"></param>
    /// <param name="authorizeResult"></param>
    /// <returns></returns>
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden)
        {
            var errorMessage = "禁止访问"; 
            if (authorizeResult.AuthorizationFailure?.FailureReasons != null && authorizeResult.AuthorizationFailure.FailureReasons.Any())
            {
                errorMessage = string.Join("；", authorizeResult.AuthorizationFailure.FailureReasons.Select(r => r.Message));
            }
            var result = ApiResult.Error(StatusCodes.Status403Forbidden, errorMessage);
            result.TraceId = context.TraceIdentifier;
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(result);
        }
        else
        {
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
