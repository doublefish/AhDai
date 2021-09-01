using Microsoft.Extensions.DependencyInjection;
using System;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ServiceCollectionExtensions
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// 数据库服务
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDbService(this IServiceCollection services)
		{
			//services.AddSingleton<Service.IDbService, Service.DbService>();
			//return services;
			throw new NotImplementedException();
		}
	}
}
