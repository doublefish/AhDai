using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.ESign;

/// <summary>
/// ESignServiceCollectionExtensions
/// </summary>
public static class ESignServiceCollectionExtensions
{
    /// <summary>
    /// AddESignService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddESignService(this IServiceCollection services, IConfiguration configuration, string key = "ESign")
    {
        services.AddOptions<Configs.ESignConfig>(configuration, key);
        services.AddScoped<IESignService, ESignService>();
        return services;
    }
}
