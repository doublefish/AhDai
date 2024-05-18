using AhDai.Core.Extensions;
using AhDai.Core.Services;
using AhDai.Core.Services.Impl;
using AutoMapper;
using AhDai.Service.Impl.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace AhDai.Service.Impl;

internal class MyStartup : IStartup
{
	/// <summary>
	/// ConfigureServices
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultDbString");
		ArgumentException.ThrowIfNullOrEmpty(connectionString);

		services.AddDbContextFactory<Db.DefaultDbContext>(options =>
		{
			var interceptors = new Db.Interceptors.MySaveChangesInterceptor(async () =>
			{
				var data = await MyApp.GetLoginDataAsync();
				return new Db.OperatingUser(data.Id, data.Username)
				{
					Name = data.Name,
					TenantId = data.TenantId,
					TenantName = data.TenantName,
					TenantType = (int)data.TenantType,
				};
			});
			options.UseSqlServer(connectionString, options =>
			{
				options.UseCompatibilityLevel(120);
			});
			options.UseLoggerFactory(LoggerFactory.Create(builder =>
			{
				var provider = AhDai.Core.Utils.ServiceUtil.Services.GetRequiredService<ILoggerProvider>();
				builder.AddConsole();
				builder.AddDebug();
				builder.AddProvider(provider);
			}));
			options.AddInterceptors(interceptors);
		});

		var configureClient = new Action<HttpClient>(c =>
		{
			c.DefaultRequestHeaders.Connection.Add("keep-alive");
			// 启用保活机制：保持活动超时设置为 2 小时，并将保持活动间隔设置为 1 秒。
			ServicePointManager.SetTcpKeepAlive(true, 7200000, 1000);
			// 默认连接数限制为2，增加连接数限制
			ServicePointManager.DefaultConnectionLimit = 512;
		});
		services.AddHttpClient("", configureClient);
		services.AddHttpClient("NoCertificateValidation", configureClient).ConfigurePrimaryHttpMessageHandler(() =>
		{
			return new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
			};
		});

		services.AddJwtService();
		services.AddSingleton<IBaseFileService, BaseFileServiceImpl>();
		services.AddSingleton<IBaseRedisService, BaseRedisServiceImpl>();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



		// 反射注入服务
		var assembly = Assembly.GetExecutingAssembly();
		var types = assembly.GetTypes();
		AddServices(services, types);

		// 反射注入AutoMapper
		AddAutoMapper(services, types);
	}

	static void AddServices(IServiceCollection services, Type[] types)
	{
		if (types == null || types.Length == 0)
		{
			return;
		}
		foreach (var type in types)
		{
			if (type == null || !type.IsClass || type.IsAbstract)
			{
				continue;
			}
			var attr = type.GetCustomAttribute<ServiceAttribute>();
			if (attr != null)
			{
				var interfaces = type.GetInterfaces();
				if (interfaces != null && interfaces.Length > 0)
				{
					foreach (var iface in interfaces)
					{
						if (iface == null || string.IsNullOrEmpty(iface.FullName))
						{
							continue;
						}
						switch (attr.Lifetime)
						{
							case ServiceLifetime.Singleton:
								services.AddSingleton(iface, type);
								break;
							case ServiceLifetime.Scoped:
								services.AddScoped(iface, type);
								break;
							case ServiceLifetime.Transient:
								services.AddTransient(iface, type);
								break;
						}
					}

				}
			}

		}
	}

	static void AddAutoMapper(IServiceCollection services, Type[] types)
	{
		if (types == null || types.Length == 0)
		{
			return;
		}
		var parentType = typeof(Profile);
		foreach (var type in types)
		{
			if (type == null || !type.IsClass || type.IsAbstract)
			{
				continue;
			}
			if (type.IsSubclassOf(parentType))
			{
				services.AddAutoMapper(type);
			}
		}
	}
}
