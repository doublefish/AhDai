using AhDai.Base.Extensions;
using AhDai.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Core.Middlewares
{
	/// <summary>
	/// RequestHandlerMiddleware
	/// </summary>
	public class RequestHandlerMiddleware
	{
		/// <summary>
		/// _Next
		/// </summary>
		readonly RequestDelegate _next;
		/// <summary>
		/// _Logger
		/// </summary>
		readonly ILogger _logger;

		readonly Stopwatch _stopwatch;

		readonly Stopwatch _stopwatch1;

		readonly JsonSerializerOptions _jsonSerializerOptions;

		readonly Configs.LogRequestConfig _config;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="next"></param>
		/// <param name="logger"></param>
		/// <param name="configuration"></param>
		/// <param name="config"></param>
		public RequestHandlerMiddleware(RequestDelegate next, ILogger<RequestHandlerMiddleware> logger, IConfiguration configuration,
			Configs.LogRequestConfig config = null)
		{
			_next = next;
			_logger = logger;
			_stopwatch = new Stopwatch();
			_stopwatch1 = new Stopwatch();
			_jsonSerializerOptions = new JsonSerializerOptions()
			{
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
				Converters = { new Utils.DatetimeJsonConverter(Const.DateTimeFormat) },
				WriteIndented = true
			};
			_config = config ?? configuration.GetSection("LogRequest").Get<Configs.LogRequestConfig>();
		}

		/// <summary>
		/// InvokeAsync
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			var requestId = context.Request.Headers[Const.RequestId];
			var eventId = new EventId(0, requestId);

			var isLog = _config != null && _config.Methods.Contains(context.Request.Method);
			if (!isLog)
			{
				await InvokeAsync(context, eventId);
				return;
			}

			_stopwatch.Restart();
			_stopwatch1.Restart();

			if (string.IsNullOrEmpty(requestId))
			{
				requestId = Guid.NewGuid().ToString();
				context.Request.Headers.Add(Const.RequestId, requestId);
			}
			var data = new Dictionary<string, object>() {
				{ "Request.Method", context.Request.Method },
				{ "Request.Path", context.Request.Path.Value ?? "" },
				{ "Request.Query", context.Request.QueryString.Value ?? "" },
				{ "Request.Headers", context.Request.Headers.ToDictionary(k => k.Key, v => string.Join(";", v.Value.ToList())) }
			};
			context.Request.EnableBuffering();
			if (context.Request.ContentType != null && !context.Request.ContentType.StartsWith("multipart/form-data"))
			{
				var text = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
				context.Request.Body.Position = 0;
				data.Add("Request.Body", text);
			}
			_stopwatch1.Stop();
			data.Add("Request.Body.ElapsedTime", _stopwatch1.ElapsedMilliseconds + "ms");

			var originalResponseBody = context.Response.Body;
			using var newResponseBody = new MemoryStream();
			context.Response.Body = newResponseBody;

			await InvokeAsync(context, eventId);

			_stopwatch1.Restart();
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			await context.Response.Body.CopyToAsync(originalResponseBody);
			_stopwatch1.Stop();
			data.Add("Response.StatusCode", context.Response.StatusCode);
			data.Add("Response.Body", responseBodyText);
			data.Add("Response.Body.ElapsedTime", _stopwatch1.ElapsedMilliseconds + "ms");

			context.Response.OnCompleted(() =>
			{
				_stopwatch.Stop();
				data.Add("ElapsedTime", _stopwatch.ElapsedMilliseconds + "ms");
				_logger.LogDebug(eventId, "接口请求=>{Message}", Utils.JsonUtil.Serialize(data, _jsonSerializerOptions));
				return Task.CompletedTask;
			});
		}


		private async Task InvokeAsync(HttpContext context, EventId eventId)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				var result = new ApiResult<string>((int)HttpStatusCode.InternalServerError, ex.Message);
				if (ex is ApiException)
				{
					var apiEx = ex as ApiException;
					result.Code = apiEx.Code;
				}
				else
				{
					_logger.LogError(eventId, ex, "请求发生异常=>{Message}", ex.Message);
				}
				await context.Response.WriteAsJsonAsync(result).ConfigureAwait(false);
			}
		}
	}
}
