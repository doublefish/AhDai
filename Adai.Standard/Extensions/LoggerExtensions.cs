using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Adai.Standard.Extensions
{
	/// <summary>
	/// LoggerExtensions
	/// </summary>
	public static class LoggerExtensions
	{
		/// <summary>
		/// LogTrace
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogTrace(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.LogTrace(eventId, null, message, args);
		}

		/// <summary>
		/// LogTrace
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogTrace(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, eventId, exception, message, args);
		}

		/// <summary>
		/// LogDebug
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogDebug(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.LogDebug(eventId, null, message, args);
		}

		/// <summary>
		/// LogDebug
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogDebug(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, eventId, exception, message, args);
		}

		/// <summary>
		/// LogInformation
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogInformation(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.LogInformation(eventId, null, message, args);
		}

		/// <summary>
		/// LogInformation
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogInformation(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, eventId, exception, message, args);
		}

		/// <summary>
		/// LogWarning
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogWarning(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.LogWarning(eventId, null, message, args);
		}

		/// <summary>
		/// LogWarning
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogWarning(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, eventId, exception, message, args);
		}

		/// <summary>
		/// LogError
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogError(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.LogError(eventId, null, message, args);
		}

		/// <summary>
		/// LogError
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogError(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, eventId, exception, message, args);
		}

		/// <summary>
		/// LogCritical
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogCritical(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.LogCritical(eventId, null, message, args);
		}

		/// <summary>
		/// LogCritical
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void LogCritical(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, eventId, exception, message, args);
		}


		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="logLevel"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Log(this ILogger logger, LogLevel logLevel, string eventId, string message, params object[] args)
		{
			logger.Log(logLevel, eventId, null, message, args);
		}

		/// <summary>
		/// Log
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="logLevel"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Log(this ILogger logger, LogLevel logLevel, string eventId, Exception exception, string message, params object[] args)
		{
			if (args != null && args.Length > 0)
			{
				message = string.Format(message, args);
			}
			var date = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
			//message = $"[{dateStr}][{Environment.ProcessId}][{eventId}]{message}";
			//logger.Log(logLevel, exception, message, args);
			logger.Log(logLevel, exception, Format, date, Environment.ProcessId, eventId, message);
		}

		const string Format = "[{date}][{process}][{eventId}]{message}";
	}
}
