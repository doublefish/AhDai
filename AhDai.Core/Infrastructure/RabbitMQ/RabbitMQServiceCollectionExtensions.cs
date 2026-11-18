using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AhDai.Core.Infrastructure.RabbitMQ;

/// <summary>
/// RabbitMQServiceCollectionExtensions
/// </summary>
public static class RabbitMQServiceCollectionExtensions
{
    /// <summary>
    /// 添加 RabbitMQ 服务 - 依赖注入单例
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddRabbitMQService(this IServiceCollection services,IConfiguration configuration,string key = "RabbitMQ")
    {
        services.AddOptions<RabbitMQOptions>(configuration, key);
        services.TryAddSingleton<IRabbitMQService, BaseRabbitMQService>();
        return services;
    }
}
