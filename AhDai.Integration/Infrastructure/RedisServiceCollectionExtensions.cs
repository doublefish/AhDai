using AhDai.Integration.Abstractions;
using AhDai.Integration.Options;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Infrastructure;

/// <summary>
/// RedisServiceCollectionExtensions
/// </summary>
public static class RedisServiceCollectionExtensions
{
    /// <summary>
    /// AddRedisKeyBuilder
    /// </summary>
    /// <param name="services"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureRedisKeyBuilder(this IServiceCollection services, string prefix = "AhDai")
    {
        services.Configure<IntegrationOptions>(options =>
        {
            options.RedisKeyPrefix = prefix;
        });
        return services;
    }

    /// <summary>
    /// AddRedisKeyBuilder
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisKeyBuilder(this IServiceCollection services)
    {
        services.AddSingleton<IRedisKeyBuilder, RedisKeyBuilder>();
        return services;
    }
}
