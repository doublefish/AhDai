using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.AntChain;

/// <summary>
/// AntChainServiceCollectionExtensions
/// </summary>
public static class AntChainServiceCollectionExtensions
{
    /// <summary>
    /// AddAntChainNotaryService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAntChainNotaryService(this IServiceCollection services, IConfiguration configuration, string key = "AntChainNotary")
    {
        services.AddOptions<Configs.AntChainNotaryConfig>(configuration, key);
        services.AddScoped<IAntChainNotaryService, AntChainNotaryService>();
        return services;
    }
}
