using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace AhDai.Core.Utils;

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
		HttpContextAccessor = services.GetService<IHttpContextAccessor>();
		HostEnvironment = services.GetService<IHostEnvironment>();
		WebHostEnvironment = services.GetService<IWebHostEnvironment>();
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
			var jwtService = Services.GetRequiredService<Services.IBaseJwtService>();
			return jwtService.ToTokenData(httpContext.User.Claims.ToArray());
		}
		return null;
	}
}
