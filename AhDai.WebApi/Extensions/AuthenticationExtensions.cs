using AhDai.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.WebApi.Extensions;

/// <summary>
/// AuthenticationExtensions
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// 添加Jwt认证服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetJwtConfig();
        services.AddJwtAuthentication(config);
        return services;
    }
}
