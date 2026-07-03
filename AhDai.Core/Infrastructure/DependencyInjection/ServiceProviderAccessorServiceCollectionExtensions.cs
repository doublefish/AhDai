using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Core.Infrastructure.DependencyInjection;

/// <summary>
/// ServiceProviderAccessorServiceCollectionExtensions
/// </summary>
public static class ServiceProviderAccessorServiceCollectionExtensions
{
    /// <summary>
    /// 添加IServiceProviderAccessor - 依赖注入单例
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServiceProviderAccessor(this IServiceCollection services)
    {
        services.AddSingleton<IServiceProviderAccessor, ServiceProviderAccessor>();
        return services;
    }
}
