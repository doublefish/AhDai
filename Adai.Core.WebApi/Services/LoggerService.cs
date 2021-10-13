using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Adai.WebApi.Services
{
	/// <summary>
	/// LoggerService
	/// </summary>
	public class LoggerService : ILogger
	{
		/// <summary>
		/// Log
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		/// <param name="logLevel"></param>
		/// <param name="eventId"></param>
		/// <param name="state"></param>
		/// <param name="exception"></param>
		/// <param name="formatter"></param>
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			var st = new StackTrace(6, true);
			var sfs = st.GetFrames();
			var methodName = string.Empty;

			if (StackFrame.OFFSET_UNKNOWN != sfs[0].GetILOffset())
			{
				var methodInfo = sfs[0].GetMethod();
				methodName = $"{methodInfo?.ReflectedType?.FullName}.{methodInfo?.Name}:{sfs[0].GetFileLineNumber()}";
			}

			var level = logLevel switch
			{
				LogLevel.Trace => "Trace",
				LogLevel.Debug => "Debug",
				LogLevel.Information => "Info",
				LogLevel.Warning => "Warn",
				LogLevel.Error => "Error",
				LogLevel.Critical => "Critical",
				LogLevel.None => "None",
				_ => string.Empty
			};
			var requestId = Activity.Current?.GetCustomProperty(Const.RequestId)?.ToString();
			Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}|{level}|{methodName}|{requestId}|{formatter(state, exception)}|{exception}");
		}

		/// <summary>
		/// BeginScope
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		/// <param name="state"></param>
		/// <returns></returns>
		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		/// <summary>
		/// IsEnabled
		/// </summary>
		/// <param name="logLevel"></param>
		/// <returns></returns>
		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}
	}
}
