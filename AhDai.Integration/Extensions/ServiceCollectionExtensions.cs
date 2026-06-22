using AhDai.Core.Extensions;
using AhDai.Integration.Abstractions;
using AhDai.Integration.Infrastructure;
using AhDai.Integration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
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

    /// <summary>
    /// AddAliyunOssService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunOssService(this IServiceCollection services, IConfiguration configuration, string key = "AliyunOss")
    {
        services.AddOptions<Aliyun.Configs.AliyunOssConfig>(configuration, key);
        services.AddScoped<Aliyun.IAliyunOssService, Aliyun.AliyunOssService>();
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
        services.AddOptions<Aliyun.Configs.AliyunVodConfig>(configuration, key);
        services.AddScoped<Aliyun.IAliyunVodService, Aliyun.AliyunVodService>();
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
        services.AddOptions<Aliyun.Configs.AliyunOcrConfig>(configuration, key);
        services.AddScoped<Aliyun.IAliyunOcrService, Aliyun.AliyunOcrService>();
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
        services.AddOptions<Aliyun.Configs.AliyunSmsConfig>(configuration, key);
        services.AddScoped<Aliyun.IAliyunSmsService, Aliyun.AliyunSmsService>();
        return services;
    }

    /// <summary>
    /// AddAmapService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAmapService(this IServiceCollection services, IConfiguration configuration, string key = "Amap")
    {
        services.AddOptions<Amap.Configs.AmapConfig>(configuration, key);
        services.AddScoped<Amap.IAmapService, Amap.AmapService>();
        return services;
    }

    /// <summary>
    /// AddAntChainNotaryService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddAntChainNotaryService(this IServiceCollection services, IConfiguration configuration, string key = "AntChainNotary")
    {
        services.AddOptions<AntChain.Configs.AntChainNotaryConfig>(configuration, key);
        services.AddScoped<AntChain.IAntChainNotaryService, AntChain.AntChainNotaryService>();
        return services;
    }

    /// <summary>
    /// AddBaiduFaceprintService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduFaceprintService(this IServiceCollection services, IConfiguration configuration, string key = "BaiduFaceprint")
    {
        services.AddOptions<Baidu.Configs.BaiduFaceprintConfig>(configuration, key);
        services.AddScoped<Baidu.IBaiduFaceprintService, Baidu.BaiduFaceprintService>();
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
        services.AddOptions<Baidu.Configs.BaiduMapConfig>(configuration, key);
        services.AddScoped<Baidu.IBaiduMapService, Baidu.BaiduMapService>();
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
        services.AddOptions<Baidu.Configs.BaiduOcrConfig>(configuration, key);
        services.AddScoped<Baidu.IBaiduOcrService, Baidu.BaiduOcrService>();
        return services;
    }

    /// <summary>
    /// AddHikIoTService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddHikIoTService(this IServiceCollection services, IConfiguration configuration, string key = "HikIoT")
    {
        services.AddOptions<Hikvision.Configs.HikIoTConfig>(configuration, key);
        services.AddScoped<Hikvision.IHikIoTService, Hikvision.HikIoTService>();
        return services;
    }

    /// <summary>
    /// AddESignService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddESignService(this IServiceCollection services, IConfiguration configuration, string key = "ESign")
    {
        services.AddOptions<ESign.Configs.ESignConfig>(configuration, key);
        services.AddScoped<ESign.IESignService, ESign.ESignService>();
        return services;
    }

    /// <summary>
    /// AddTianyanchaService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddTianyanchaService(this IServiceCollection services, IConfiguration configuration, string key = "Tianyancha")
    {
        services.AddOptions<Tianyancha.Configs.TianyanchaConfig>(configuration, key);
        services.AddScoped<Tianyancha.ITianyanchaService, Tianyancha.TianyanchaService>();
        return services;
    }

    /// <summary>
    /// AddTencentMapService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddTencentMapService(this IServiceCollection services, IConfiguration configuration, string key = "TencentMap")
    {
        services.AddOptions<Tencent.Configs.TencentMapConfig>(configuration, key);
        services.AddScoped<Tencent.ITencentMapService, Tencent.TencentMapService>();
        return services;
    }

    /// <summary>
    /// AddWeChatMiniProgramService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatMiniProgramService(this IServiceCollection services, IConfiguration configuration, string key = "WeChatMiniProgram")
    {
        services.AddOptions<WeChat.Configs.WeChatMiniProgramConfig>(configuration, key);
        services.AddScoped<WeChat.IWeChatMiniProgramService, WeChat.WeChatMiniProgramService>();
        return services;
    }

    /// <summary>
    /// AddWeChatOfficialAccountService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatOfficialAccountService(this IServiceCollection services, IConfiguration configuration, string key = "WeChatOfficialAccount")
    {
        services.AddOptions<WeChat.Configs.WeChatOfficialAccountConfig>(configuration, key);
        services.AddScoped<WeChat.IWeChatOfficialAccountService, WeChat.WeChatOfficialAccountService>();
        return services;
    }

    /// <summary>
    /// AddWeChatPayService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatPayService(this IServiceCollection services, IConfiguration configuration, string key = "WeChatPay")
    {
        services.AddOptions<WeChat.Configs.WeChatPayConfig>(configuration, key);
        services.AddScoped<WeChat.IWeChatPayService, WeChat.WeChatPayService>();
        return services;
    }

    /// <summary>
    /// AddWeChatWebAppService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatWebAppService(this IServiceCollection services, IConfiguration configuration, string key = "WeChatWebApp")
    {
        services.AddOptions<WeChat.Configs.WeChatWebAppConfig>(configuration, key);
        services.AddScoped<WeChat.IWeChatWebAppService, WeChat.WeChatWebAppService>();
        return services;
    }
}
