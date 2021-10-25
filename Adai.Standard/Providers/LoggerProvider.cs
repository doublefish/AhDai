using Microsoft.Extensions.Logging;
using System;

namespace Adai.Standard.Providers
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
			var logger = new Services.LoggerService();
			//Console.WriteLine($"CreateLogger=>{logger.EventId}");
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
