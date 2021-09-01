using Microsoft.Extensions.DependencyInjection;
using System;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ServiceHelper
	/// </summary>
	public static class ServiceHelper
	{
		/// <summary>
		/// ServiceCollection
		/// </summary>
		public static IServiceCollection ServiceCollection { get; set; }
		/// <summary>
		/// ServiceProvider
		/// </summary>
		public static IServiceProvider ServiceProvider { get; set; }

	}
}
