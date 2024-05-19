﻿using AhDai.Core;
using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.WebApi.Extensions;

/// <summary>
/// AuthenticationExtensions
/// </summary>
public static class AuthenticationExtensions
{
	/// <summary>
	/// 添加Jwt认证服务
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var config = configuration.GetJwtConfig();
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
					var token = context.Request.Headers.Authorization.ToString();
					if (!string.IsNullOrEmpty(token) && config.EnableRedis)
					{
						var jwtService = ServiceUtil.Services.GetRequiredService<AhDai.Core.Services.IBaseJwtService>();
						var exists = await jwtService.ExistsTokenAsync(token);
						if (!exists)
						{
							context.Fail(new Exception("认证失效"));
						}
					}
				},
				// 此处为权限验证失败后触发的事件
				OnChallenge = context =>
				{
					// 此处代码为终止.Net Core默认的返回类型和数据结果
					context.HandleResponse();

					// 自定义返回内容
					var requestId = context.Request.Headers[Const.RequestId];
					var result = ApiResult.Error<string>(StatusCodes.Status401Unauthorized, "未认证或认证失效");

					context.Response.WriteAsJsonAsync(result);
					return Task.FromResult(0);
				}
			};
		});
		return services;
	}
}