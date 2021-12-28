using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adai.Standard.Extensions
{
	/// <summary>
	/// HostExtensions
	/// </summary>
	public static class HostExtensions
	{
		/// <summary>
		/// ConfigureHostBuilder
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IHostBuilder ConfigureHostBuilder(this IHostBuilder builder)
		{
			return builder.ConfigureAppConfiguration((hostingContext, builder) =>
			{

			});
		}

		/// <summary>
		/// ConfigureLogging
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="onExecuted"></param>
		/// <returns></returns>
		public static IHostBuilder ConfigureLogging(this IHostBuilder builder, Action<LogLevel, string, Exception> onExecuted = null)
		{
			return builder.ConfigureLogging((context, builder) =>
			{
				builder.AddLoggerProvider(onExecuted);
			});
		}
	}
}
