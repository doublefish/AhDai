using AhDai.Core.Handlers;
using AhDai.Core.Requirements;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
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
        services.AddSingleton<Services.IBaseRedisService, Services.BaseRedisService>();
        return services;
    }

    /// <summary>
    /// AddJwtService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtService(this IServiceCollection services)
    {
        return services.AddSingleton<Services.IBaseJwtService, Services.BaseJwtService>();
    }

    /// <summary>
    /// AddFileService
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFileService(this IServiceCollection services)
    {
        return services.AddSingleton<Services.IBaseFileService, Services.BaseFileService>();
    }

    /// <summary>
    /// 添加Jwt认证服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder services, Configs.JwtConfig config)
    {
        return services.AddJwtBearer(options =>
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(config.PublicKey), out _);
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
                // 密钥
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.PrivateKey)),
                IssuerSigningKey = new RsaSecurityKey(rsa),
                // 是否验证生命周期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ValidateLifetime = true,
                // 过期时间，是否要求Token的Claims中必须包含Expires
                RequireExpirationTime = false,
                // 允许服务器时间偏移量
                ClockSkew = TimeSpan.FromMinutes(config.ClockSkew)
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    if (config.EnableRedis)
                    {
                        var token = context.Request.Headers.Authorization.ToString();
                        var jwtService = ServiceUtil.Services.GetRequiredService<Services.IBaseJwtService>();
                        var exists = await jwtService.ValidateTokenAsync(token);
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
    }

    /// <summary>
    /// 添加通用授权服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddGenericAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, GenericClaimHandler>();
        //services.AddAuthorizationBuilder().SetDefaultPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new ClaimRequirement("", "")).Build());
        return services;
    }

}
