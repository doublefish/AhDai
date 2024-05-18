using AhDai.Core.Utils;
using AhDai.Db;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// MyApp
/// </summary>
public static class MyApp
{
	static readonly AsyncLocal<LoginData> AsyncLocalUser = new();
	static readonly AsyncLocal<string> AsyncLocalRootPath = new();

	public static void SetOperatingUser(LoginData user)
	{
		AsyncLocalUser.Value = user;
	}

	/// <summary>
	/// Services
	/// </summary>
	public static IServiceProvider Services => ServiceUtil.Services;
	/// <summary>
	/// 文件根路径
	/// </summary>
	/// <returns></returns>
	public static string WebRootPath => ServiceUtil.WebHostEnvironment.WebRootPath;

	/// <summary>
	/// GetServerAddresses
	/// </summary>
	/// <returns></returns>
	public static ICollection<string> GetServerAddresses()
	{
		var server = ServiceUtil.Services.GetRequiredService<IServer>();
		if (server != null)
		{
			var features = server.Features.Get<IServerAddressesFeature>();
			if (features != null)
			{
				return features.Addresses;
			}
		}
		throw new Exception("获取服务地址失败");
	}

	/// <summary>
	/// 获取当前请求的IP地址
	/// </summary>
	/// <returns></returns>
	public static string GetRequestLocalIpAddress(bool toIPv6 = false)
	{
		var httpContext = ServiceUtil.HttpContextAccessor.HttpContext;
		if (httpContext != null)
		{
			var iPAddress = httpContext.Connection.LocalIpAddress;
			if (iPAddress != null)
			{
				var value = toIPv6 ? iPAddress.MapToIPv6() : iPAddress.MapToIPv4();
				return value.ToString();
			}
		}
		return "";
	}

	/// <summary>
	/// 获取当前请求的根路径
	/// </summary>
	/// <returns></returns>
	public static string GetRequestRootPath()
	{
		var root = AsyncLocalRootPath.Value;
		if (root == null)
		{
			var httpContext = ServiceUtil.HttpContextAccessor.HttpContext;
			if (httpContext != null)
			{
				root = $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}";
				AsyncLocalRootPath.Value = root;
			}
		}
		return root ?? "";
	}

	/// <summary>
	/// 获取全路径
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static string GetFullPath(string path)
	{
		var root = GetRequestRootPath();
		return $"{root}/{path}";
	}

	/// <summary>
	/// GetLogger
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static ILogger GetLogger<T>()
	{
		return LoggerUtil.GetLogger<T>();
	}

	/// <summary>
	/// GetLogger
	/// </summary>
	/// <param name="categoryName"></param>
	/// <returns></returns>
	public static ILogger GetLogger(string categoryName)
	{
		return LoggerUtil.GetLogger(categoryName);
	}

	/// <summary>
	/// 获取当前登录数据
	/// </summary>
	/// <returns></returns>
	public static async Task<LoginData> GetLoginDataAsync()
	{
		var user = AsyncLocalUser.Value;
		if (user == null)
		{
			var authService = ServiceUtil.Services.GetRequiredService<IAuthService>();
			user = await authService.GetLoginAsync();
			AsyncLocalUser.Value = user;
		}
		if (user == null)
		{
			throw new Exception("未读取到用户信息");
		}
		return user;
	}

	/// <summary>
	/// 获取当前登录用户信息
	/// </summary>
	/// <returns></returns>
	public static async Task<AccountOutput> GetLoginUserAsync()
	{
		var authService = ServiceUtil.Services.GetRequiredService<IAccountService>();
		return await authService.GetAsync();
	}

	/// <summary>
	/// GetDefaultDbAsync
	/// </summary>
	/// <returns></returns>
	public static async Task<DefaultDbContext> GetDefaultDbAsync()
	{
		var factory = ServiceUtil.Services.GetRequiredService<IDbContextFactory<DefaultDbContext>>();
		return await factory.CreateDbContextAsync();
	}
}
