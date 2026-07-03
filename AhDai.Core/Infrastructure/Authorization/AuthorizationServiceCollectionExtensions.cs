using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Core.Infrastructure.Authorization;

/// <summary>
/// AuthorizationServiceCollectionExtensions
/// </summary>
public static class AuthorizationServiceCollectionExtensions
{ /// <summary>
  /// 添加权限授权服务
  /// </summary>
  /// <param name="services"></param>
  /// <param name="requirements"></param>
  /// <returns></returns>
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services, params IAuthorizationRequirement[] requirements)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, PermissionAuthorizationMiddlewareResultHandler>();
        services.AddAuthorizationBuilder().SetDefaultPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(requirements).Build());
        return services;
    }

}
