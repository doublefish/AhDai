using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adai.Standard
{
	/// <summary>
	/// LoggerHelper
	/// </summary>
	public static class LoggerHelper
	{
		/// <summary>
		/// Logger
		/// </summary>
		public static ILogger Logger { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="logger"></param>
		public static void Init(ILogger logger)
		{
			Logger = logger;
		}

		/// <summary>
		/// Trace
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Trace(string message, Exception exception = null)
		{
			Logger.LogTrace(exception, message);
		}

		/// <summary>
		/// Debug
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Debug(string message, Exception exception = null)
		{
			Logger.LogDebug(exception, message);
		}

		/// <summary>
		/// Info
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Info(string message, Exception exception = null)
		{
			Logger.LogInformation(exception, message);
		}

		/// <summary>
		/// Warn
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Warn(string message, Exception exception = null)
		{
			Logger.LogWarning(exception, message);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Error(string message, Exception exception = null)
		{
			Logger.LogError(exception, message);
		}

		/// <summary>
		/// Critical
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Critical(string message, Exception exception = null)
		{
			Logger.LogCritical(exception, message);
		}
	}
}
