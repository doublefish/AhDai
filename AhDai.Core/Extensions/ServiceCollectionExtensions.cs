using AhDai.Core.Models;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Core.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加Redis服务 - 依赖注入单例
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisService(this IServiceCollection services)
    {
        services.AddSingleton<Services.IBaseRedisService, Services.Impl.BaseRedisService>();
        return services;
    }

    /// <summary>
    /// AddJwtService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtService(this IServiceCollection services)
    {
        return services.AddSingleton<Services.IBaseJwtService, Services.Impl.BaseJwtService>();
    }

    /// <summary>
    /// AddFileService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFileService(this IServiceCollection services)
    {
        return services.AddSingleton<Services.IBaseFileService, Services.Impl.BaseFileService>();
    }

    /// <summary>
    /// 添加Jwt认证服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Configs.JwtConfig config)
    {
        services.AddAuthentication(options =>
        {
            //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                // 是否验证签发人
                ValidateIssuer = true,
                ValidIssuer = config.Issuer,
                // 是否验证受众
                ValidateAudience = true,
                ValidAudience = config.Audience,
                // 是否验证密钥
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SigningKey)),
                // 是否验证生命周期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ValidateLifetime = true,
                // 过期时间，是否要求Token的Claims中必须包含Expires
                RequireExpirationTime = false,
                // 允许服务器时间偏移量
                ClockSkew = TimeSpan.FromSeconds(config.ClockSkew)
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    if (config.EnableRedis)
                    {
                        var jwtService = ServiceUtil.Services.GetRequiredService<Services.IBaseJwtService>();
                        var exists = await jwtService.ValidateTokenAsync(context);
                        if (!exists) context.Fail(new Exception("认证失效"));
                    }
                },
                // 此处为权限验证失败后触发的事件
                OnChallenge = context =>
                {
                    // 终止默认的返回类型和数据结果
                    context.HandleResponse();

                    // 自定义返回内容
                    var result = ApiResult.Error(StatusCodes.Status401Unauthorized, "未认证或认证失效");
                    result.TraceId = context.HttpContext.TraceIdentifier;
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.WriteAsJsonAsync(result);
                    return Task.FromResult(0);
                }
            };
        });
        return services;
    }

}
