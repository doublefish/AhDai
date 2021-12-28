using Adai.Standard.Extensions;
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
		readonly Action<LogLevel, string, Exception> OnExecuted;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="onExecuted"></param>
		public LoggerService(Action<LogLevel, string, Exception> onExecuted = null)
		{
			OnExecuted = onExecuted;
			//Console.WriteLine($"new LoggerService=>{EventId}");
		}

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
			var st = new StackTrace(3, true);
			var sfs = st.GetFrames();
			var trace = string.Empty;
			var offset = sfs[0].GetILOffset();
			foreach (var sf in sfs)
			{
				var method = sf.GetMethod();
				var fullName = method.DeclaringType.FullName;
				if (StackFrame.OFFSET_UNKNOWN == offset ||
					fullName.StartsWith("Microsoft.Extensions.Logging")
					|| fullName == "Adai.Standard.Extensions.LoggerExtensions")
				{
					continue;
				}
				trace = $"{method?.ReflectedType?.FullName}.{method?.Name}:{sf.GetFileLineNumber()}";
				break;
			}

			var levelName = logLevel.GetName();
			var message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{Environment.ProcessId}][{Environment.CurrentManagedThreadId}] {levelName}: {trace}[{eventId.Id}]"
				+ $"\r\n      {formatter(state, exception)}"
				+ $"\r\n      {exception}";

			Console.WriteLine(message);
			OnExecuted?.Invoke(logLevel, message, exception);
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
