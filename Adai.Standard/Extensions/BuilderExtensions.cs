using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Adai.Standard.Extensions
{
	/// <summary>
	/// BuilderExtensions
	/// </summary>
	public static class BuilderExtensions
	{
		#region UseDbContext
		/// <summary>
		/// UseDbContext
		/// </summary>
		/// <param name="app"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseDbContext(this IApplicationBuilder app, Options.DbContextOptions options)
		{
			if (options != null)
			{
				Utils.DbContextHelper.Init(options.Configs);
			}
			return app;
		}

		/// <summary>
		/// UseDbContext
		/// </summary>
		/// <param name="app"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseDbContext(this IApplicationBuilder app, Action<Options.DbContextOptions> setupAction = null)
		{
			Options.DbContextOptions options;
			using (var scope = app.ApplicationServices.CreateScope())
			{
				// 这里才会执行添加配置时传入的action
				options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options.DbContextOptions>>().Value;
				setupAction?.Invoke(options);
			}
			return app.UseDbContext(options);
		}
		#endregion

		#region UseRedis
		/// <summary>
		/// UseRedis
		/// </summary>
		/// <param name="app"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseRedis(this IApplicationBuilder app, Options.RedisOptions options)
		{
			if (options != null)
			{
				Utils.RedisHelper.Init(options.Config);
			}
			return app;
		}

		/// <summary>
		/// UseRedis
		/// </summary>
		/// <param name="app"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseRedis(this IApplicationBuilder app, Action<Options.RedisOptions> setupAction = null)
		{
			Options.RedisOptions options;
			using (var scope = app.ApplicationServices.CreateScope())
			{
				// 这里才会执行添加配置时传入的action
				options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options.RedisOptions>>().Value;
				setupAction?.Invoke(options);
			}
			return app.UseRedis(options);
		}
		#endregion

		#region UseRabbitMQ
		/// <summary>
		/// UseRabbitMQ
		/// </summary>
		/// <param name="app"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseRabbitMQ(this IApplicationBuilder app, Options.RabbitMQOptions options)
		{
			if (options != null)
			{
				RabbitMQ.Helper.Init(options.Config);
			}
			return app;
		}

		/// <summary>
		/// UseRabbitMQ
		/// </summary>
		/// <param name="app"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseRabbitMQ(this IApplicationBuilder app, Action<Options.RabbitMQOptions> setupAction = null)
		{
			Options.RabbitMQOptions options;
			using (var scope = app.ApplicationServices.CreateScope())
			{
				// 这里才会执行添加配置时传入的action
				options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options.RabbitMQOptions>>().Value;
				setupAction?.Invoke(options);
			}
			return app.UseRabbitMQ(options);
		}
		#endregion

		#region UseMail
		/// <summary>
		/// UseMail
		/// </summary>
		/// <param name="app"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseMail(this IApplicationBuilder app, Options.MailOptions options)
		{
			if (options != null)
			{
				Utils.MailHelper.Init(options.Config);
			}
			return app;
		}

		/// <summary>
		/// UseMail
		/// </summary>
		/// <param name="app"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseMail(this IApplicationBuilder app, Action<Options.MailOptions> setupAction = null)
		{
			Options.MailOptions options;
			using (var scope = app.ApplicationServices.CreateScope())
			{
				// 这里才会执行添加配置时传入的action
				options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options.MailOptions>>().Value;
				setupAction?.Invoke(options);
			}
			return app.UseMail(options);
		}
		#endregion

		/// <summary>
		/// AddLoggerProvider
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static ILoggingBuilder AddLoggerProvider(this ILoggingBuilder builder)
		{
			// 删除所有 ILoggerProvider 实例
			builder.ClearProviders();
			return builder.AddProvider(new Providers.LoggerProvider());
		}
	}
}
