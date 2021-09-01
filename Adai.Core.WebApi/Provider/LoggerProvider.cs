using Microsoft.Extensions.Logging;
using System;

namespace Adai.Core.WebApi.Provider
{
	/// <summary>
	/// LoggerProvider
	/// </summary>
	public class LoggerProvider : ILoggerProvider
	{
		/// <summary>
		/// CreateLogger
		/// </summary>
		/// <param name="categoryName"></param>
		/// <returns></returns>
		public ILogger CreateLogger(string categoryName)
		{
			var logger = new Service.LoggerService();
			Standard.LoggerHelper.Init(logger);
			return logger;
		}

		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
