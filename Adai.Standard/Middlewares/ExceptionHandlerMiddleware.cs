using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Adai.Standard.Middlewares
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
				if (Activity.Current != null)
				{
					var requestId = context.Request.Headers[Const.RequestId];
					if (!string.IsNullOrEmpty(requestId))
					{
						Activity.Current.SetCustomProperty(Const.RequestId, requestId);
					}
				}
				await next(context);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
				await HandleExceptionAsync(context, ex);
			}
		}

		/// <summary>
		/// HandleExceptionAsync
		/// </summary>
		/// <param name="context"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			exception = Adai.Extensions.ExceptionExtensions.GetInner(exception);
			var requestId = context.Request.Headers[Const.RequestId];
			var result = new Models.ActionResult<string>(requestId, 1, exception.Message);
			if (exception is Models.CustomException)
			{
				var ex = exception as Models.CustomException;
				result.Code = ex.Code;
			}
			else
			{
				Utils.LoggerHelper.Error(requestId, exception.Message, exception);
			}

			var contentType = context.Response?.ContentType?.ToLower();
			if (!string.IsNullOrEmpty(contentType) && (contentType == HttpContentType.Xml || contentType == HttpContentType.TextHtml))
			{
				await context.Response.WriteAsync(Adai.Utils.XmlHelper.SerializeObject(result)).ConfigureAwait(false);
			}
			else
			{
				context.Response.ContentType = HttpContentType.Json;
				await context.Response.WriteAsync(Utils.JsonHelper.Serialize(result)).ConfigureAwait(false);
			}
		}
	}
}
