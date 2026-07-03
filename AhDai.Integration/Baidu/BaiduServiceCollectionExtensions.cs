using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Baidu;

/// <summary>
/// BaiduServiceCollectionExtensions
/// </summary>
public static class BaiduServiceCollectionExtensions
{
    /// <summary>
    /// AddBaiduFaceprintService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduFaceprintService(this IServiceCollection services, IConfiguration configuration, string key = "BaiduFaceprint")
    {
        services.AddOptions<Configs.BaiduFaceprintConfig>(configuration, key);
        services.AddScoped<IBaiduFaceprintService, BaiduFaceprintService>();
        return services;
    }

    /// <summary>
    /// AddBaiduMapService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduMapService(this IServiceCollection services, IConfiguration configuration, string key = "BaiduMap")
    {
        services.AddOptions<Configs.BaiduMapConfig>(configuration, key);
        services.AddScoped<IBaiduMapService, BaiduMapService>();
        return services;
    }

    /// <summary>
    /// AddBaiduOcrService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduOcrService(this IServiceCollection services, IConfiguration configuration, string key = "BaiduOcr")
    {
        services.AddOptions<Configs.BaiduOcrConfig>(configuration, key);
        services.AddScoped<IBaiduOcrService, BaiduOcrService>();
        return services;
    }
}
