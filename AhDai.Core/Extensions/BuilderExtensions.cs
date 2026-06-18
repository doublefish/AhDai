using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AhDai.Core.Extensions;

/// <summary>
/// BuilderExtensions
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Configure
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder ConfigureOptions<TOptions>(this IHostApplicationBuilder builder, string key)
        where TOptions : class
    {
        builder.Services.Configure<TOptions>(builder.Configuration.GetSection(key));
        //builder.Services.AddOptions<TOptions>().Bind(builder.Configuration.GetSection(key)).PostConfigure(o =>
        //{
        //}).ValidateDataAnnotations().ValidateOnStart();
        return builder;
    }

    /// <summary>
    /// 添加IServiceProviderAccessor - 依赖注入单例
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddServiceProviderAccessor(this IHostApplicationBuilder builder)
    {
        builder.Services.AddServiceProviderAccessor();
        return builder;
    }

    /// <summary>
    /// AddFileService
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddFileService(this IHostApplicationBuilder builder, string key = "File")
    {
        builder.ConfigureOptions<Options.FileOptions>(key);
        builder.Services.AddFileService();
        return builder;
    }

    /// <summary>
    /// AddJwtService
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddJwtService(this IHostApplicationBuilder builder, string key = "Jwt")
    {
        builder.ConfigureOptions<Options.JwtOptions>(key);
        builder.Services.AddJwtService();
        return builder;
    }

    /// <summary>
    /// AddMailService
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddMailService(this IHostApplicationBuilder builder, string key = "Mail")
    {
        builder.ConfigureOptions<Options.MailOptions>(key);
        builder.Services.AddMailService();
        return builder;
    }

    /// <summary>
    /// 添加Redis服务 - 依赖注入单例
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddRedisService(this IHostApplicationBuilder builder, string key = "Redis")
    {
        builder.ConfigureOptions<Options.RedisOptions>(key);
        builder.Services.AddRedisService();
        return builder;
    }

    /// <summary>
    /// 添加通用授权服务
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddGenericAuthorization(this IHostApplicationBuilder builder)
    {
        builder.Services.AddPermissionAuthorization();
        return builder;
    }
}
