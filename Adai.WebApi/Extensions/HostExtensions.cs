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
			//Standard.Extensions.HostExtensions.ConfigureHostBuilder(builder);
			builder.ConfigureLogging((context, builder) =>
			{
				Standard.Extensions.BuilderExtensions.AddLoggerProvider(builder);
			});
			return builder.ConfigureAppConfiguration((hostingContext, builder) =>
			{

			});
		}
	}
}
