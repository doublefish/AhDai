using Microsoft.Extensions.Logging;
using System;

namespace Adai.Standard.Utils
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
		/// <param name="requestId"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Trace(string requestId, string message, Exception exception = null)
		{
			message = FormatMessage(requestId, message);
			Logger.LogTrace(exception, message);
		}

		/// <summary>
		/// Debug
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Debug(string requestId, string message, Exception exception = null)
		{
			message = FormatMessage(requestId, message);
			Logger.LogDebug(exception, message);
		}

		/// <summary>
		/// Info
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Info(string requestId, string message, Exception exception = null)
		{
			message = FormatMessage(requestId, message);
			Logger.LogInformation(exception, message);
		}

		/// <summary>
		/// Warn
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Warn(string requestId, string message, Exception exception = null)
		{
			message = FormatMessage(requestId, message);
			Logger.LogWarning(exception, message);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Error(string requestId, string message, Exception exception = null)
		{
			message = FormatMessage(requestId, message);
			Logger.LogError(exception, message);
		}

		/// <summary>
		/// Critical
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public static void Critical(string requestId, string message, Exception exception = null)
		{
			message = FormatMessage(requestId, message);
			Logger.LogCritical(exception, message);
		}

		/// <summary>
		/// 格式化消息
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		static string FormatMessage(string requestId, string message)
		{
			return $"[{requestId}]{message}";
		}
	}
}
