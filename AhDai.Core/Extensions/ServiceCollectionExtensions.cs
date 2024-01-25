using AhDai.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Core.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions
    /// </summary>
    public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// 添加数据库服务 - 依赖注入单例
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDbService(this IServiceCollection services)
		{
			services.AddSingleton<Services.IDbService, Services.Impl.DbService>();
			return services;
		}

		/// <summary>
		/// 添加Redis服务 - 依赖注入单例
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddRedisService(this IServiceCollection services)
		{
			services.AddSingleton<Services.IRedisService, Services.Impl.RedisService>();
			return services;
		}

		/// <summary>
		/// AddJwtService
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddJwtService(this IServiceCollection services)
		{
			return services.AddSingleton<Services.IJwtService, Services.Impl.JwtService>();
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
					OnTokenValidated = context =>
					{
						var token = context.Request.Headers.Authorization.ToString();
						if (!string.IsNullOrEmpty(token) && config.EnableRedis)
						{
							var jwtService = ServiceUtil.Services.GetRequiredService<Services.IJwtService>();
							var exists = jwtService.ExistsTokenAsync(token).Result;
							if (!exists)
							{
								context.Fail(new Exception("认证失效"));
							}
						}
						return Task.CompletedTask;
					},
					// 此处为权限验证失败后触发的事件
					OnChallenge = context =>
					{
						// 此处代码为终止.Net Core默认的返回类型和数据结果
						context.HandleResponse();

						// 自定义返回内容
						var requestId = context.Request.Headers[Const.RequestId];
						var result = new ApiResult<string>(StatusCodes.Status401Unauthorized, "未认证或认证失效");

						context.Response.WriteAsJsonAsync(result);
						return Task.FromResult(0);
					}
				};
			});
			return services;
		}


		#region DbContext
		/// <summary>
		/// 注册数据库服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddDbContext(this IServiceCollection services, Action<Options.DbContextOptions> setupAction = null)
		{
			if (setupAction != null)
			{
				services.ConfigureDbContext(setupAction);
			}
			return services;
		}

		/// <summary>
		/// 注册数据库服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		public static void ConfigureDbContext(this IServiceCollection services, Action<Options.DbContextOptions> setupAction)
		{
			services.Configure(setupAction);
		}
		#endregion

		#region Redis
		/// <summary>
		/// 注册Redis服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddRedis(this IServiceCollection services, Action<Options.RedisOptions> setupAction = null)
		{
			if (setupAction != null)
			{
				services.ConfigureRedis(setupAction);
			}
			return services;
		}

		/// <summary>
		/// 配置Redis服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		public static void ConfigureRedis(this IServiceCollection services, Action<Options.RedisOptions> setupAction)
		{
			services.Configure(setupAction);
		}
		#endregion

		#region 邮箱
		/// <summary>
		/// 注册邮箱服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddMail(this IServiceCollection services, Action<Options.MailOptions> setupAction = null)
		{
			if (setupAction != null)
			{
				services.ConfigureMail(setupAction);
			}
			return services;
		}

		/// <summary>
		/// 配置邮箱服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		public static void ConfigureMail(this IServiceCollection services, Action<Options.MailOptions> setupAction)
		{
			services.Configure(setupAction);
		}
		#endregion
	}
}
