using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Core.Infrastructure.Jwt;

/// <summary>
/// JwtServiceCollectionExtensions
/// </summary>
public static class JwtServiceCollectionExtensions
{

    /// <summary>
    /// AddJwtService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration, string key = "Jwt")
    {
        services.AddOptions<JwtOptions>(configuration, key);
        services.AddSingleton<IBaseJwtService, BaseJwtService>();
        return services;
    }
}
