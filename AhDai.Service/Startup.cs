using AhDai.Core.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace AhDai.Service;

/// <summary>
/// Startup
/// </summary>
public static class Startup
{
	/// <summary>
	/// ConfigureServices
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
	{
		var startupType = typeof(IStartup);
		try
		{
			var assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AhDai.Service.Impl.dll"));
			if (assembly != null)
			{
				var types = assembly.GetTypes();
				if (types != null)
				{
					foreach (var item in types)
					{
						if (item.IsInterface || !item.IsClass || item.IsAbstract)
						{
							continue;
						}
						var interfaces = item.GetInterfaces();
						foreach (var i in interfaces)
						{
							if (i == startupType)
							{
								var obj = Activator.CreateInstance(item);
								if (obj == null)
								{
									continue;
								}
								var s = (IStartup)obj;
								s.ConfigureServices(services, configuration);
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("启动异常=>" + ex.Message);
		}

		return services;
	}

	/// <summary>
	/// Configure
	/// </summary>
	/// <param name="app"></param>
	public static void Configure(IApplicationBuilder app)
	{
	}

	/// <summary>
	/// Configure
	/// </summary>
	/// <param name="app"></param>
	public static void Configure(WebApplication app)
	{
		// 另存服务实例
		ServiceUtil.Init(app.Services, app.Configuration);
	}

	/// <summary>
	/// Init
	/// </summary>
	/// <param name="host"></param>
	public static void Configure(IHost host)
	{
		// 另存服务实例
		ServiceUtil.Init(host.Services, host.Services.GetRequiredService<IConfiguration>());
	}
}
