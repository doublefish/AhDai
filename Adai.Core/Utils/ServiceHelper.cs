using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Adai.Core.Utils
{
	/// <summary>
	/// ServiceHelper
	/// </summary>
	public static class ServiceHelper
	{
		/// <summary>
		/// 服务实例
		/// </summary>
		public static IServiceProvider Instance { get; private set; }
		/// <summary>
		/// HttpContextAccessor
		/// </summary>
		public static IHttpContextAccessor HttpContextAccessor { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="instance"></param>
		public static void Init(IServiceProvider instance)
		{
			Instance = instance;
			HttpContextAccessor = GetService<IHttpContextAccessor>();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="services"></param>
		public static void Init(IServiceCollection services)
		{
			Init(services.BuildServiceProvider());
		}

		/// <summary>
		/// 获取指定类型的范围服务的实例（通过 AddScoped 添加的）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetScopedService<T>()
		{
			return HttpContextAccessor.HttpContext.RequestServices.GetService<T>();
		}

		/// <summary>
		/// 获取注入的（范围或单例）服务的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetService<T>()
		{
			return Instance.GetService<T>();
		}

		/// <summary>
		/// 获取注入的（范围或单例）服务的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetRequiredService<T>()
		{
			return Instance.GetRequiredService<T>();
		}
	}
}
