using AhDai.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace AhDai.Core.Utils
{
	/// <summary>
	/// ServiceHelper
	/// </summary>
	public static class ServiceUtil
	{
		/// <summary>
		/// 服务实例
		/// </summary>
		public static IServiceProvider Services { get; private set; }
		/// <summary>
		/// Configuration
		/// </summary>
		public static IConfiguration Configuration { get; private set; }
		/// <summary>
		/// HttpContextAccessor
		/// </summary>
		public static IHttpContextAccessor HttpContextAccessor { get; private set; }
		/// <summary>
		/// HostEnvironment
		/// </summary>
		public static IHostEnvironment HostEnvironment { get; private set; }
		/// <summary>
		/// HostEnvironment
		/// </summary>
		public static IWebHostEnvironment WebHostEnvironment { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		public static void Init(IServiceCollection services, IConfiguration configuration)
		{
			Init(services.BuildServiceProvider(), configuration);
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		public static void Init(IServiceProvider services, IConfiguration configuration)
		{
			Services = services;
			Configuration = configuration;
			HttpContextAccessor = GetService<IHttpContextAccessor>();
			HostEnvironment = GetService<IHostEnvironment>();
			WebHostEnvironment = GetService<IWebHostEnvironment>();
		}

		/// <summary>
		/// 获取指定类型的范围服务的实例（通过 AddScoped 添加的）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetScopedService<T>()
		{
			return HttpContextAccessor.HttpContext.RequestServices.GetService<T>();
		}

		/// <summary>
		/// 获取注入的（范围或单例）服务的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetService<T>()
		{
			return Services.GetService<T>();
		}

		/// <summary>
		/// 获取注入的（范围或单例）服务的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetRequiredService<T>()
		{
			return Services.GetRequiredService<T>();
		}

		/// <summary>
		/// 获取当前Token数据
		/// </summary>
		/// <returns></returns>
		public static Models.TokenData GetCurrentTokenData()
		{
			var httpContext = HttpContextAccessor.HttpContext;
			if (httpContext != null && httpContext.User != null)
			{
				var jwtService = GetRequiredService<Services.IJwtService>();
				return jwtService.ToTokenData(httpContext.User.Claims.ToArray());
			}
			return null;
		}
	}
}
