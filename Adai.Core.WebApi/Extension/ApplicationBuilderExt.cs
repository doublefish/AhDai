using Adai.Extension;
using Adai.Standard;
using Adai.Standard.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ApplicationBuilderExt
	/// </summary>
	public static class ApplicationBuilderExt
	{
		/// <summary>
		/// 启用数据库服务
		/// </summary>
		/// <param name="app"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseDbContext(this IApplicationBuilder app, IConfiguration config)
		{
			//var res = DbHelper.Init(config);
			//if (res == false)
			//{
			//	throw new Exception("DbHelper初始化失败，请检查配置项");
			//}
			return app;
		}

		/// <summary>
		/// 启用Redis
		/// </summary>
		/// <param name="app"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseRedis(this IApplicationBuilder app, IConfiguration config)
		{
			var _config = new RedisConfiguration()
			{
				Host = config.GetSection("redis:host").Value,
				Port = config.GetSection("redis:port").Value.ToInt32(),
				Password = config.GetSection("redis:password").Value
			};
			var res = RedisHelper.Init(_config);
			if (res == false)
			{
				throw new Exception("RedisHelper初始化失败，请检查配置项");
			}
			return app;
		}

		/// <summary>
		/// 启用邮件
		/// </summary>
		/// <param name="app"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseMail(this IApplicationBuilder app, IConfiguration config)
		{
			var _config = new MailConfiguration()
			{
				Host = config.GetSection("mail:smtp:host").Value,
				Port = config.GetSection("mail:smtp:port").Value.ToInt32(),
				Username = config.GetSection("mail:smtp:username").Value,
				Password = config.GetSection("mail:smtp:password").Value
			};
			var res = MailHelper.Init(_config);
			if (res == false)
			{
				throw new Exception("MailHelper初始化失败，请检查配置项");
			}
			return app;
		}

		/// <summary>
		/// UseExceptionHandler
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ExceptionHandlerMiddleware>();
		}

		/// <summary>
		/// UseExceptionHandler
		/// </summary>
		/// <param name="app"></param>
		/// <param name="errorHandlingPath"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, string errorHandlingPath)
		{
			return app.UseExceptionHandler(new ExceptionHandlerOptions
			{
				ExceptionHandlingPath = new PathString(errorHandlingPath)
			});
		}

		/// <summary>
		/// UseExceptionHandler
		/// </summary>
		/// <param name="app"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, ExceptionHandlerOptions options)
		{
			return app.UseMiddleware<ExceptionHandlerMiddleware>(Options.Create(options));
		}
	}
}
