using AhDai.Core.Handlers;
using AhDai.Core.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhDai.Core.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Infrastructure

    /// <summary>
    /// 添加IServiceProviderAccessor - 依赖注入单例
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServiceProviderAccessor(this IServiceCollection services)
    {
        services.AddSingleton<Interfaces.Infrastructure.IServiceProviderAccessor, Infrastructure.ServiceProviderAccessor>();
        return services;
    }

    #endregion

    #region Authorization

    /// <summary>
    /// 添加权限授权服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="requirements"></param>
    /// <returns></returns>
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services, params IAuthorizationRequirement[] requirements)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, PermissionAuthorizationMiddlewareResultHandler>();
        services.AddAuthorizationBuilder().SetDefaultPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(requirements).Build());
        return services;
    }

    #endregion

    #region Options

    /// <summary>
    /// AddOptions
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string key)
        where TOptions : class
    {
        services.Configure<TOptions>(configuration.GetSection(key));
        //services.AddOptions<TOptions>().Bind(configuration.GetSection(key)).PostConfigure(o =>
        //{
        //}).ValidateDataAnnotations().ValidateOnStart();
        return services;
    }

    #endregion

    #region Services

    /// <summary>
    /// AddFileService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddFileService(this IServiceCollection services, IConfiguration configuration, string key = "File")
    {
        services.AddOptions<Options.FileOptions>(configuration, key);
        services.AddSingleton<Interfaces.Services.IBaseFileService, Services.BaseFileService>();
        return services;
    }

    /// <summary>
    /// AddJwtService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration, string key = "Jwt")
    {
        services.AddOptions<Options.JwtOptions>(configuration, key);
        services.AddSingleton<Interfaces.Services.IBaseJwtService, Services.BaseJwtService>();
        return services;
    }

    /// <summary>
    /// AddMailService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration configuration, string key = "Mail")
    {
        services.AddOptions<Options.MailOptions>(configuration, key);
        services.AddSingleton<Interfaces.Services.IBaseMailService, Services.BaseMailService>();
        return services;
    }

    /// <summary>
    /// 添加Redis服务 - 依赖注入单例
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration, string key = "Redis")
    {
        services.AddOptions<Options.RedisOptions>(configuration, key);
        services.AddSingleton<Interfaces.Services.IBaseRedisService, Services.BaseRedisService>();
        return services;
    }

    #endregion
}
