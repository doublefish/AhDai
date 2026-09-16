using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AhDai.Core.Infrastructure.MessageBus;

/// <summary>
/// MessageBusServiceCollectionExtensions
/// </summary>
public static class MessageBusServiceCollectionExtensions
{
    /// <summary>
    /// 添加IMessageBus - 依赖注入单例
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisMessageBus(this IServiceCollection services)
    {
        services.TryAddSingleton<IMessageBus, RedisMessageBus>();
        return services;
    }
}
