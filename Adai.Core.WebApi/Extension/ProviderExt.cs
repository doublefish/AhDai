using Microsoft.Extensions.Logging;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ProviderExt
	/// </summary>
	public static class ProviderExt
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
			return builder.AddProvider(new Provider.LoggerProvider());
		}
	}
}
