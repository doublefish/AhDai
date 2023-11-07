using AhDai.Base.Extensions;
using AhDai.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AhDai.Core.Middlewares
{
	/// <summary>
	/// ExceptionHandlerMiddleware
	/// </summary>
	public class ExceptionHandlerMiddleware
	{
		/// <summary>
		/// _Next
		/// </summary>
		readonly RequestDelegate next;
		/// <summary>
		/// _Logger
		/// </summary>
		readonly ILogger logger;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="next"></param>
		/// <param name="logger"></param>
		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}

		/// <summary>
		/// Invoke
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				var requestId = context.Request.Headers[Const.RequestId];
				var eventId = new EventId(0, requestId);
				logger.LogDebug(eventId, $"收到请求=>{context.Request.Method} {context.Request.Path} {context.Request.QueryString}");
				await next(context);
				logger.LogDebug(eventId, $"返回结果=>{context.Response}");
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex, logger);
			}
		}

		/// <summary>
		/// HandleExceptionAsync
		/// </summary>
		/// <param name="context"></param>
		/// <param name="exception"></param>
		/// <param name="logger"></param>
		/// <returns></returns>
		static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
		{
			var requestId = context.Request.Headers[Const.RequestId];
			var result = new Models.ActionResult<string>(requestId, 1, exception.Message);
			if (exception is Models.CustomException)
			{
				var ex = exception as Models.CustomException;
				result.Code = ex.Code;
			}
			else
			{
				var eventId = new EventId(0, requestId);
				logger.LogError(eventId, exception, $"请求发生异常=>{exception.Message}");
			}

			await context.Response.WriteObjectAsync(result).ConfigureAwait(false);
		}
	}
}
