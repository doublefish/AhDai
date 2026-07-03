using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Hikvision;

/// <summary>
/// HikvisionServiceCollectionExtensions
/// </summary>
public static class HikvisionServiceCollectionExtensions
{
    /// <summary>
    /// AddHikIoTService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddHikIoTService(this IServiceCollection services, IConfiguration configuration, string key = "HikIoT")
    {
        services.AddOptions<Configs.HikIoTConfig>(configuration, key);
        services.AddScoped<IHikIoTService, HikIoTService>();
        return services;
    }
}
