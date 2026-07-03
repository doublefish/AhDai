using AhDai.Core.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Aliyun;

/// <summary>
/// AliyunServiceCollectionExtensions
/// </summary>
public static class AliyunServiceCollectionExtensions
{
    /// <summary>
    /// AddAliyunOssService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunOssService(this IServiceCollection services, IConfiguration configuration, string key = "AliyunOss")
    {
        services.AddOptions<Configs.AliyunOssConfig>(configuration, key);
        services.AddScoped<IAliyunOssService, AliyunOssService>();
        return services;
    }

    /// <summary>
    /// AddAliyunVodService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunVodService(this IServiceCollection services, IConfiguration configuration, string key = "AliyunVod")
    {
        services.AddOptions<Configs.AliyunVodConfig>(configuration, key);
        services.AddScoped<IAliyunVodService, AliyunVodService>();
        return services;
    }

    /// <summary>
    /// AddAliyunOcrService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunOcrService(this IServiceCollection services, IConfiguration configuration, string key = "AliyunOcr")
    {
        services.AddOptions<Configs.AliyunOcrConfig>(configuration, key);
        services.AddScoped<IAliyunOcrService, AliyunOcrService>();
        return services;
    }

    /// <summary>
    /// AddAliyunSmsService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunSmsService(this IServiceCollection services, IConfiguration configuration, string key = "AliyunSms")
    {
        services.AddOptions<Configs.AliyunSmsConfig>(configuration, key);
        services.AddScoped<IAliyunSmsService, AliyunSmsService>();
        return services;
    }
}
