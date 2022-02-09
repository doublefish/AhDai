using Microsoft.Extensions.Hosting;

namespace Adai.WebApi.Extensions
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
			Core.Extensions.HostExtensions.ConfigureHostBuilder(builder);
			builder.ConfigureLogging((context, builder) =>
			{
				Core.Extensions.BuilderExtensions.AddLoggerProvider(builder);
			});
			builder.ConfigureAppConfiguration((hostingContext, builder) =>
			{

			});
			return builder;
		}
	}
}
