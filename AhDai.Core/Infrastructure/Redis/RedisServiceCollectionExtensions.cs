using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AhDai.Core.Infrastructure.Redis;

/// <summary>
/// RedisServiceCollectionExtensions
/// </summary>
public static class RedisServiceCollectionExtensions
{
    /// <summary>
    /// 添加Redis服务 - 依赖注入单例
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration, string key = "Redis")
    {
        services.AddOptions<RedisOptions>(configuration, key);
        services.TryAddSingleton<IBaseRedisService, BaseRedisService>();
        return services;
    }
}
