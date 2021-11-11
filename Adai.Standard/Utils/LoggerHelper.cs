using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Adai.Standard.Utils
{
	/// <summary>
	/// LoggerHelper
	/// </summary>
	public static class LoggerHelper
	{
		/// <summary>
		/// 
		/// </summary>
		public static ILoggerProvider Provider { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		static LoggerHelper()
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
			if (type == 1)
			{
				var factory = ServiceHelper.Instance.GetService<ILoggerFactory>();
				// 复用同名对象
				return factory.CreateLogger(categoryName ?? "");
			}
			else
			{
				var provider = ServiceHelper.Instance.GetService<ILoggerProvider>();
				var logger = provider.CreateLogger(categoryName);
				return logger;
			}
		}
	}
}
