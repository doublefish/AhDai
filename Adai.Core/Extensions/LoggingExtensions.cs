using Microsoft.Extensions.Logging;
using System;

namespace Adai.Core.Extensions
{
	/// <summary>
	/// LoggingExtensions
	/// </summary>
	public static class LoggingExtensions
	{
		/// <summary>
		/// LogTrace
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Trace(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.Trace(eventId, null, message, args);
		}

		/// <summary>
		/// LogTrace
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Trace(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
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
		public static void Debug(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.Debug(eventId, null, message, args);
		}

		/// <summary>
		/// LogDebug
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Debug(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
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
		public static void Information(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.Information(eventId, null, message, args);
		}

		/// <summary>
		/// LogInformation
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Information(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
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
		public static void Warning(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.Warning(eventId, null, message, args);
		}

		/// <summary>
		/// LogWarning
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Warning(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
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
		public static void Error(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.Error(eventId, null, message, args);
		}

		/// <summary>
		/// LogError
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Error(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
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
		public static void Critical(this ILogger logger, string eventId, string message, params object[] args)
		{
			logger.Critical(eventId, null, message, args);
		}

		/// <summary>
		/// LogCritical
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="eventId"></param>
		/// <param name="exception"></param>
		/// <param name="message"></param>
		/// <param name="args"></param>
		public static void Critical(this ILogger logger, string eventId, Exception exception, string message, params object[] args)
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
			logger.Log(logLevel, exception, "[{eventId}]{message}", eventId, message);
		}

		/// <summary>
		/// GetName
		/// </summary>
		/// <param name="logLevel"></param>
		/// <returns></returns>
		public static string GetName(this LogLevel logLevel)
		{
			return logLevel switch
			{
				LogLevel.Trace => "Trace",
				LogLevel.Debug => "Debug",
				LogLevel.Information => "Info",
				LogLevel.Warning => "Warn",
				LogLevel.Error => "Error",
				LogLevel.Critical => "Critical",
				_ => string.Empty
			};
		}
	}
}
