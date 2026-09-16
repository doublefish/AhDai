using AhDai.Core.Infrastructure.DependencyInjection;
using AhDai.Core.Infrastructure.Redis;
using AhDai.Integration.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Integration.WeChat;

/// <summary>
/// WeChatServiceCollectionExtensions
/// </summary>
public static class WeChatServiceCollectionExtensions
{
    /// <summary>
    /// AddWeChatMiniProgramService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddWeChatMiniProgramService(this IServiceCollection services, IConfiguration configuration, string key = "WeChatMiniProgram")
    {
        services.AddOptions<Configs.WeChatMiniProgramConfig>(configuration, key);
        services.AddRedisService(configuration);
        services.AddRedisKeyBuilder();
        services.AddScoped<IWeChatMiniProgramService, WeChatMiniProgramService>();
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
        services.AddOptions<Configs.WeChatOfficialAccountConfig>(configuration, key);
        services.AddRedisService(configuration);
        services.AddRedisKeyBuilder();
        services.AddScoped<IWeChatOfficialAccountService, WeChatOfficialAccountService>();
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
        services.AddOptions<Configs.WeChatPayConfig>(configuration, key);
        services.AddScoped<IWeChatPayService, WeChatPayService>();
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
        services.AddOptions<Configs.WeChatWebAppConfig>(configuration, key);
        services.AddRedisService(configuration);
        services.AddRedisKeyBuilder();
        services.AddScoped<IWeChatWebAppService, WeChatWebAppService>();
        return services;
    }
}
