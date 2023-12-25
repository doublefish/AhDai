using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AhDai.Core.Utils;

/// <summary>
/// LoggerUtil
/// </summary>
public static class LoggerUtil
{
	/// <summary>
	/// 构造函数
	/// </summary>
	static LoggerUtil()
	{

	}

	/// <summary>
	/// GetLogger
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static ILogger GetLogger<T>()
	{
		return GetLogger(typeof(T).FullName);
	}

	/// <summary>
	/// GetLogger
	/// </summary>
	/// <param name="categoryName"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public static ILogger GetLogger(string categoryName = null, int type = 0)
	{
		if (ServiceUtil.Services == null)
		{
			throw new Exception("未初始化ServiceHelper.Services");
		}
		if (type == 1)
		{
			var factory = ServiceUtil.Services.GetService<ILoggerFactory>();
			// 复用同名对象
			return factory.CreateLogger(categoryName ?? "");
		}
		else
		{
			var provider = ServiceUtil.Services.GetService<ILoggerProvider>();
			var logger = provider.CreateLogger(categoryName);
			return logger;
		}
	}
}
