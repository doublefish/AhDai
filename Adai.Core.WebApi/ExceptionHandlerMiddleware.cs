using Adai.Extension;
using Adai.Standard;
using Adai.Standard.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Adai.Core.WebApi.Models;

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
		readonly RequestDelegate _Next;
		/// <summary>
		/// _Logger
		/// </summary>
		readonly ILogger _Logger;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="next"></param>
		/// <param name="logger"></param>
		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			_Next = next;
			_Logger = logger;
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
				await _Next(context).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				_Logger.LogError(ex, ex.Message);
				await HandleExceptionAsync(context, ex).ConfigureAwait(false);
			}
		}

		/// <summary>
		/// HandleExceptionAsync
		/// </summary>
		/// <param name="context"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			//var path = context.Request.Path.Value;
			//var type = context.Request.ContentType;
			//var length = context.Request.ContentLength;
			//var datas = context.Request.GetParameters();

			exception = exception.GetInner();
			var result = new ReturnResult<string>()
			{
				Message = exception.Message
			};
			if (exception is CustomException)
			{
				if (exception.Message == "login_timeout")
				{
					result.Code = ReturnCode.LoginTimeout;
					result.Message = "登录超时。";
				}
				else
				{
					result.Code = ReturnCode.CustomException;
				}
				result.Message = string.Format("[CustomError]{0}", result.Message);
			}
			else if (exception is ApiException)
			{
				result.Code = ReturnCode.ApiException;
				result.Message = string.Format("[ApiError]{0}", result.Message);
				Log4netHelper.Error(result.Message, exception);
			}
			else
			{
				result.Code = ReturnCode.SystemException;
				result.Message = string.Format("[SystemError]{0}", result.Message);
				Log4netHelper.Error(result.Message, exception);
			}

			context.Response.ContentType = HttpContentType.Json;
			context.Response.WriteAsync(JsonHelper.SerializeObject(result));
			return Task.CompletedTask;
		}
	}
}
