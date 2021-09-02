using Adai.Extension;
using Adai.Standard;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Adai.Core.WebApi
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
			exception = exception.GetInner();
			var result = new ReturnResult<string>(1, exception.Message);
			if (exception is Adai.Standard.Model.CustomException)
			{
				var ex = exception as Adai.Standard.Model.CustomException;
				result.Code = ex.Code;
			}
			else
			{
				LoggerHelper.Error(exception.Message, exception);
			}

			var contentType = context.Response?.ContentType?.ToLower();
			if (!string.IsNullOrEmpty(contentType) && (contentType == HttpContentType.Xml || contentType == HttpContentType.TextHtml))
			{
				var sw = new StringWriter();
				try
				{
					var serializer = new XmlSerializer(result.GetType());
					serializer.Serialize(sw, result);
				}
				finally
				{
					sw.Dispose();
				}
				await context.Response.WriteAsync(sw.ToString()).ConfigureAwait(false);
			}
			else
			{
				context.Response.ContentType = HttpContentType.Json;
				await context.Response.WriteAsync(JsonHelper.SerializeObject(result)).ConfigureAwait(false);
			}
		}
	}
}
