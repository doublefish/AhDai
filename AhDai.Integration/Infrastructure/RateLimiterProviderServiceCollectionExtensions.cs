using AhDai.Integration.Abstractions;
using AhDai.Integration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AhDai.Integration.Extensions;

/// <summary>
/// RateLimiterProviderServiceCollectionExtensions
/// </summary>
public static class RateLimiterProviderServiceCollectionExtensions
{
    /// <summary>
    /// AddRateLimiter
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRateLimiterProvider(this IServiceCollection services)
    {
        services.TryAddSingleton<IRateLimiterProvider, RateLimiterProvider>();
        return services;
    }
}
