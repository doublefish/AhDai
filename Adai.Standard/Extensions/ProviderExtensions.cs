using Microsoft.Extensions.Logging;

namespace Adai.Standard.Extensions
{
	/// <summary>
	/// ProviderExtensions
	/// </summary>
	public static class ProviderExtensions
	{
		/// <summary>
		/// AddLoggerProvider
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static ILoggingBuilder AddLoggerProvider(this ILoggingBuilder builder)
		{
			// 清空所有
			builder.ClearProviders();
			return builder.AddProvider(new Providers.LoggerProvider());
		}
	}
}
