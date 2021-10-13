using Microsoft.Extensions.DependencyInjection;
using System;

namespace Adai.Standard.Utils
{
	/// <summary>
	/// ServiceHelper
	/// </summary>
	public static class ServiceHelper
	{
		/// <summary>
		/// Services
		/// </summary>
		public static IServiceCollection Services { get; private set; }

		/// <summary>
		/// Provider
		/// </summary>
		public static IServiceProvider Provider { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="services"></param>
		public static void Init(IServiceCollection services)
		{
			Services = services;
			Provider = services.BuildServiceProvider();
		}

		/// <summary>
		/// GetScope
		/// </summary>
		/// <param name="provider"></param>
		/// <returns></returns>
		public static IServiceScope GetScope(IServiceProvider provider = null)
		{
			provider ??= Provider;
			return provider?.GetRequiredService<IServiceScopeFactory>().CreateScope();
		}
	}
}
