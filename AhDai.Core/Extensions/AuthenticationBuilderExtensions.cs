using AhDai.Core.Models;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AhDai.Core.Extensions;

/// <summary>
/// AuthenticationBuilderExtensions
/// </summary>
public static class AuthenticationBuilderExtensions
{
    /// <summary>
    /// 添加Jwt认证服务
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="jwtOptions"></param>
    /// <returns></returns>
    public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder builder, Options.JwtOptions jwtOptions)
    {
        return builder.AddJwtBearer(options =>
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(jwtOptions.PublicKey), out _);
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                // 是否验证签发人
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                // 是否验证受众
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
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
                ClockSkew = TimeSpan.FromMinutes(jwtOptions.ClockSkew)
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    if (jwtOptions.EnableRedis)
                    {
                        var token = context.Request.Headers.Authorization.ToString();
                        var jwtService = ServiceUtil.Services.GetRequiredService<Interfaces.Services.IBaseJwtService>();
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
}
