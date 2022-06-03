﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace AhDai.Core.Extensions
{
	/// <summary>
	/// ServiceCollectionExtensions
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// 添加数据库服务 - 依赖注入单例
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDbService(this IServiceCollection services)
		{
			services.AddSingleton<Interfaces.IDbService, Services.DbService>();
			return services;
		}

		/// <summary>
		/// 添加Redis服务 - 依赖注入单例
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddRedisService(this IServiceCollection services)
		{
			services.AddSingleton<Interfaces.IRedisService, Services.RedisService>();
			return services;
		}

		/// <summary>
		/// 添加RabbitMQ服务 - 依赖注入单例
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddRabbitMQService(this IServiceCollection services)
		{
			services.AddSingleton<Interfaces.IRabbitMQService, Services.RabbitMQService>();
			return services;
		}

		/// <summary>
		/// 添加订阅服务 - 依赖注入单例
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddSubscribeService(this IServiceCollection services)
		{
			services.AddSingleton<Interfaces.ISubscribeService, Services.SubscribeService>();
			return services;
		}

		/// <summary>
		/// 添加发布服务 - 依赖注入瞬时
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddPublishService(this IServiceCollection services)
		{
			services.AddTransient<Interfaces.IPublishService, Services.PublishService>();
			return services;
		}

		#region DbContext
		/// <summary>
		/// 注册数据库服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddDbContext(this IServiceCollection services, Action<Options.DbContextOptions> setupAction = null)
		{
			if (setupAction != null)
			{
				services.ConfigureDbContext(setupAction);
			}
			return services;
		}

		/// <summary>
		/// 注册数据库服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		public static void ConfigureDbContext(this IServiceCollection services, Action<Options.DbContextOptions> setupAction)
		{
			services.Configure(setupAction);
		}
		#endregion

		#region Redis
		/// <summary>
		/// 注册Redis服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddRedis(this IServiceCollection services, Action<Options.RedisOptions> setupAction = null)
		{
			if (setupAction != null)
			{
				services.ConfigureRedis(setupAction);
			}
			return services;
		}

		/// <summary>
		/// 配置Redis服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		public static void ConfigureRedis(this IServiceCollection services, Action<Options.RedisOptions> setupAction)
		{
			services.Configure(setupAction);
		}
		#endregion

		#region RabbitMQ
		/// <summary>
		/// 注册RabbitMQ服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddRabbitMQ(this IServiceCollection services, Action<Options.RabbitMQOptions> setupAction = null)
		{
			if (setupAction != null)
			{
				services.ConfigureRabbitMQ(setupAction);
			}
			return services;
		}

		/// <summary>
		/// 配置RabbitMQ服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		public static void ConfigureRabbitMQ(this IServiceCollection services, Action<Options.RabbitMQOptions> setupAction)
		{
			services.Configure(setupAction);
		}
		#endregion

		#region 邮箱
		/// <summary>
		/// 注册邮箱服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddMail(this IServiceCollection services, Action<Options.MailOptions> setupAction = null)
		{
			if (setupAction != null)
			{
				services.ConfigureMail(setupAction);
			}
			return services;
		}

		/// <summary>
		/// 配置邮箱服务
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		public static void ConfigureMail(this IServiceCollection services, Action<Options.MailOptions> setupAction)
		{
			services.Configure(setupAction);
		}
		#endregion
	}
}