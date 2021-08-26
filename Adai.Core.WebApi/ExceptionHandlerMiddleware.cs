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
		readonly RequestDelegate _next;
		/// <summary>
		/// _LoggerFactory
		/// </summary>
		readonly ILoggerFactory _loggerFactory;
		/// <summary>
		/// _Options
		/// </summary>
		readonly ExceptionHandlerOptions _options;
		/// <summary>
		/// _DiagnosticSource
		/// </summary>
		readonly DiagnosticSource _diagnosticSource;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="next"></param>
		/// <param name="loggerFactory"></param>
		/// <param name="options"></param>
		/// <param name="diagnosticSource"></param>
		public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<ExceptionHandlerOptions> options, DiagnosticSource diagnosticSource)
		{
			_next = next;
			_options = options.Value;
			_loggerFactory = loggerFactory;
			_diagnosticSource = diagnosticSource;
			if (_options != null && _diagnosticSource != null)
			{

			}
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
				await _next(context).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				_loggerFactory.CreateLogger("Default").LogTrace(ex, ex.Message);
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
				LogHelper.Error(result.Message, exception);
			}
			else
			{
				result.Code = ReturnCode.SystemException;
				result.Message = string.Format("[SystemError]{0}", result.Message);
				LogHelper.Error(result.Message, exception);
			}

			context.Response.ContentType = HttpContentType.Json;
			context.Response.WriteAsync(JsonHelper.SerializeObject(result));
			return Task.CompletedTask;
		}
	}
}
