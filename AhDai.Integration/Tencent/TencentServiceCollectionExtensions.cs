using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Tencent;

/// <summary>
/// TencentServiceCollectionExtensions
/// </summary>
public static class TencentServiceCollectionExtensions
{
    /// <summary>
    /// AddTencentMapService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddTencentMapService(this IServiceCollection services, IConfiguration configuration, string key = "TencentMap")
    {
        services.AddOptions<Configs.TencentMapConfig>(configuration, key);
        services.AddScoped<ITencentMapService, TencentMapService>();
        return services;
    }
}
