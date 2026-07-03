using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Tianyancha;

/// <summary>
/// TianyanchaServiceCollectionExtensions
/// </summary>
public static class TianyanchaServiceCollectionExtensions
{
    /// <summary>
    /// AddTianyanchaService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddTianyanchaService(this IServiceCollection services, IConfiguration configuration, string key = "Tianyancha")
    {
        services.AddOptions<Configs.TianyanchaConfig>(configuration, key);
        services.AddScoped<ITianyanchaService, TianyanchaService>();
        return services;
    }
}
