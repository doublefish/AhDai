using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// ExceptionHandlerExtensions
	/// </summary>
	public static class ExceptionHandlerExtensions
	{
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
