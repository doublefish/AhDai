using AhDai.Integration.AntChain;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// AddAliyunOssService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunOssService(this IServiceCollection services)
    {
        services.AddScoped<Aliyun.IAliyunOssService, Aliyun.AliyunOssService>();
        return services;
    }

    /// <summary>
    /// AddAliyunVodService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunVodService(this IServiceCollection services)
    {
        services.AddScoped<Aliyun.IAliyunVodService, Aliyun.AliyunVodService>();
        return services;
    }

    /// <summary>
    /// AddAliyunOcrService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunOcrService(this IServiceCollection services)
    {
        services.AddScoped<Aliyun.IAliyunOcrService, Aliyun.AliyunOcrService>();
        return services;
    }

    /// <summary>
    /// AddAliyunSmsService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAliyunSmsService(this IServiceCollection services)
    {
        services.AddScoped<Aliyun.IAliyunSmsService, Aliyun.AliyunSmsService>();
        return services;
    }

    /// <summary>
    /// AddAmapService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAmapService(this IServiceCollection services)
    {
        services.AddScoped<Amap.IAmapService, Amap.AmapService>();
        return services;
    }

    /// <summary>
    /// AddAntChainNotaryService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAntChainNotaryService(this IServiceCollection services)
    {
        services.AddScoped<IAntChainNotaryService, AntChainNotaryService>();
        return services;
    }

    /// <summary>
    /// AddBaiduService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduService(this IServiceCollection services)
    {
        services.AddScoped<Baidu.IBaiduService, Baidu.BaiduService>();
        return services;
    }

    /// <summary>
    /// AddBaiduMapService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduMapService(this IServiceCollection services)
    {
        services.AddScoped<Baidu.IBaiduMapService, Baidu.BaiduMapService>();
        return services;
    }

    /// <summary>
    /// AddBaiduFaceprintService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduFaceprintService(this IServiceCollection services)
    {
        services.AddScoped<Baidu.IBaiduFaceprintService, Baidu.BaiduFaceprintService>();
        return services;
    }

    /// <summary>
    /// AddBaiduOcrService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBaiduOcrService(this IServiceCollection services)
    {
        services.AddScoped<Baidu.IBaiduOcrService, Baidu.BaiduOcrService>();
        return services;
    }

    /// <summary>
    /// AddTencentMapService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTencentMapService(this IServiceCollection services)
    {
        services.AddScoped<Tencent.ITencentMapService, Tencent.TencentMapService>();
        return services;
    }

    /// <summary>
    /// AddWeChatMiniProgramService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatMiniProgramService(this IServiceCollection services)
    {
        services.AddScoped<WeChat.IWeChatMiniProgramService, WeChat.WeChatMiniProgramService>();
        return services;
    }

    /// <summary>
    /// AddWeChatOfficialAccountService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatOfficialAccountService(this IServiceCollection services)
    {
        services.AddScoped<WeChat.IWeChatOfficialAccountService, WeChat.WeChatOfficialAccountService>();
        return services;
    }

    /// <summary>
    /// AddWeChatPayService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatPayService(this IServiceCollection services)
    {
        services.AddScoped<WeChat.IWeChatPayService, WeChat.WeChatPayService>();
        return services;
    }

    /// <summary>
    /// AddWeChatWebAppService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatWebAppService(this IServiceCollection services)
    {
        services.AddScoped<WeChat.IWeChatWebAppService, WeChat.WeChatWebAppService>();
        return services;
    }

    /// <summary>
    /// AddESignService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddESignService(this IServiceCollection services)
    {
        services.AddScoped<ESign.IESignService, ESign.ESignService>();
        return services;
    }

    /// <summary>
    /// AddTianyanchaService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTianyanchaService(this IServiceCollection services)
    {
        services.AddScoped<Tianyancha.ITianyanchaService, Tianyancha.TianyanchaService>();
        return services;
    }
}
