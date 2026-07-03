using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Amap;

/// <summary>
/// AmapServiceCollectionExtensions
/// </summary>
public static class AmapServiceCollectionExtensions
{
    /// <summary>
    /// AddAmapService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAmapService(this IServiceCollection services, IConfiguration configuration, string key = "Amap")
    {
        services.AddOptions<Configs.AmapConfig>(configuration, key);
        services.AddScoped<IAmapService, AmapService>();
        return services;
    }
}
