using AhDai.Core.Extensions;
using AhDai.Core.Models;
using AhDai.Core.Services;
using AhDai.Core.Services.Impl;
using AhDai.Core.Utils;
using AhDai.Service.Impl;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AhDai.Service
{
	public static class MyApp
	{
		/// <summary>
		/// 添加业务服务
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddServices(IServiceCollection services)
		{
			services.AddHttpClient("", c =>
			{
				c.DefaultRequestHeaders.Connection.Add("keep-alive");
				// 启用保活机制：保持活动超时设置为 2 小时，并将保持活动间隔设置为 1 秒。
				ServicePointManager.SetTcpKeepAlive(true, 7200000, 1000);
				// 默认连接数限制为2，增加连接数限制
				ServicePointManager.DefaultConnectionLimit = 512;
			});

			services.AddJwtService();
			services.AddSingleton<IRedisService, RedisService>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddScoped<IDictService, DictServiceImpl>();
			services.AddScoped<IFileService, FileServiceImpl>();
			services.AddScoped<IAuthService, AuthServiceImpl>();
			services.AddScoped<IInterfaceService, InterfaceServiceImpl>();

			return services;
		}

		/// <summary>
		/// 获取当前Token数据
		/// </summary>
		/// <returns></returns>
		public static TokenData GetCurrentTokenData()
		{
			var httpContext = ServiceUtil.HttpContextAccessor.HttpContext;
			if (httpContext != null && httpContext.User != null)
			{
				var jwtService = ServiceUtil.Services.GetRequiredService<IJwtService>();
				return jwtService.ToTokenData(httpContext.User.Claims.ToArray());
			}
			return null;
		}

		/// <summary>
		/// GetServerAddresses
		/// </summary>
		/// <returns></returns>
		public static ICollection<string> GetServerAddresses()
		{
			var server = ServiceUtil.Services.GetRequiredService<IServer>();
			return server.Features.Get<IServerAddressesFeature>().Addresses;
		}

		/// <summary>
		/// 文件根路径
		/// </summary>
		/// <returns></returns>
		public static string WebRootPath => ServiceUtil.WebHostEnvironment.WebRootPath;

	}
}
