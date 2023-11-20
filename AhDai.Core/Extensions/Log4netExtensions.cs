using log4net.Core;
using Microsoft.Extensions.Logging;

namespace AhDai.Core.Extensions
{
	/// <summary>
	/// Log4netExtensions
	/// </summary>
	public static class Log4netExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="logLevel"></param>
		/// <returns></returns>
		public static Level ToLog4netLevel(this LogLevel logLevel)
		{
			return logLevel switch
			{
				LogLevel.Trace => Level.Trace,
				LogLevel.Debug => Level.Debug,
				LogLevel.Information => Level.Info,
				LogLevel.Warning => Level.Warn,
				LogLevel.Error => Level.Error,
				LogLevel.Critical => Level.Critical,
				//LogLevel.None => Level.Off,
				_ => null
			};
		}
	}
}
