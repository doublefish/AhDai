using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Adai.Standard.Services
{
	/// <summary>
	/// LoggerService
	/// </summary>
	public class LoggerService : ILogger
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public LoggerService()
		{
			EventId = Guid.NewGuid().ToString();
			this.LogInformation($"初始化=>{EventId}");
		}

		/// <summary>
		/// EventId
		/// </summary>
		public string EventId { get; private set; }

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
			var trace = string.Empty;
			if (StackFrame.OFFSET_UNKNOWN != sfs[0].GetILOffset())
			{
				var methodInfo = sfs[0].GetMethod();
				trace = $"{methodInfo?.ReflectedType?.FullName}.{methodInfo?.Name}:{sfs[0].GetFileLineNumber()}";
			}

			var levelName = logLevel switch
			{
				LogLevel.Trace => "trce",
				LogLevel.Debug => "dbug",
				LogLevel.Information => "info",
				LogLevel.Warning => "warn",
				LogLevel.Error => "fail",
				LogLevel.Critical => "crit",
				_ => string.Empty
			};
			var msg = $"{levelName}: {trace}[{eventId.Id}]"
				+ $"\r\n      {formatter(state, exception)}"
				+ $"\r\n      {exception}";
			Console.WriteLine(msg);
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
