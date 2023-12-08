using AhDai.Core.Extensions;
using AhDai.Core.Models;
using AhDai.Core.Services;
using AhDai.Core.Services.Impl;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1
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
			//services.AddHttpClient();
			services.AddJwtService();
			services.AddSingleton<IFileService, FileService>();
			services.AddSingleton<IRedisService, RedisService>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//services.AddScoped<Services.IDictService, Services.Impl.DictServiceImpl>();
			//services.AddScoped<Services.IFileService, Services.Impl.FileServiceImpl>();
			//services.AddScoped<Services.IAuthService, Services.Impl.AuthServiceImpl>();
			//services.AddScoped<Services.IInterfaceService, Services.Impl.InterfaceServiceImpl>();
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
				var jwtService = ServiceUtil.GetRequiredService<IJwtService>();
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
			var server = ServiceUtil.GetRequiredService<IServer>();
			return server.Features.Get<IServerAddressesFeature>().Addresses;
		}

		/// <summary>
		/// 文件根路径
		/// </summary>
		/// <returns></returns>
		public static string WebRootPath => ServiceUtil.WebHostEnvironment.WebRootPath;

	}
}
