using AhDai.Base.Extensions;
using AhDai.Core;
using AhDai.Core.Configs;
using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace AhDai.WebApi.Middlewares
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

        readonly LogRequestConfig _config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="config"></param>
        public RequestHandlerMiddleware(RequestDelegate next, ILogger<RequestHandlerMiddleware> logger, IConfiguration configuration,
            LogRequestConfig? config = null)
        {
            _next = next;
            _logger = logger;
            _stopwatch = new Stopwatch();
            _stopwatch1 = new Stopwatch();
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Converters = { new DatetimeJsonConverter(Const.DateTimeFormat) },
                WriteIndented = true
            };
            _config = config ?? configuration.GetSection("LogRequest").Get<LogRequestConfig>() ?? new LogRequestConfig();
        }

        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            _stopwatch1.Restart();
            var requestId = context.Request.Headers[Const.RequestId];
            if (string.IsNullOrEmpty(requestId))
            {
                requestId = Guid.NewGuid().ToString();
                context.Request.Headers.Append(Const.RequestId, requestId);
            }
            var eventId = new EventId(0, requestId);

            var requestData = new Dictionary<string, object>() {
                { "Method", context.Request.Method },
                { "Path", context.Request.Path.Value ?? "" },
                { "Query", context.Request.QueryString.Value ?? "" },
                { "Headers", context.Request.Headers.ToDictionary(k => k.Key, v => string.Join(";", [.. v.Value])) }
            };
            context.Request.EnableBuffering();
            if (context.Request.ContentType != null && !context.Request.ContentType.StartsWith("multipart/form-requestData")
                && !context.Request.ContentType.StartsWith("multipart/form-data; boundary="))
            {
                var text = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                context.Request.Body.Position = 0;
                requestData.Add("Body", text);
            }
            _stopwatch1.Stop();
            requestData.Add("ElapsedTime", _stopwatch1.ElapsedMilliseconds + "ms");
            _logger.LogDebug(eventId, "[{RequestId}]接收请求=>{Message}", requestId, JsonUtil.Serialize(requestData, _jsonSerializerOptions));

            var isLog = _config != null && _config.Methods.Contains(context.Request.Method);
            if (!isLog)
            {
                await InvokeAsync(context, eventId);
                return;
            }

            _stopwatch.Restart();

            await InvokeAsync(context, eventId);

            var originalResponseBody = context.Response.Body;
            using var newResponseBody = new MemoryStream();
            context.Response.Body = newResponseBody;
            _stopwatch1.Restart();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await context.Response.Body.CopyToAsync(originalResponseBody);
            _stopwatch1.Stop();

            var responseData = new Dictionary<string, object>
            {
                { "StatusCode", context.Response.StatusCode },
                { "Body", responseBodyText },
                { "Body.ElapsedTime", _stopwatch1.ElapsedMilliseconds + "ms" }
            };

            context.Response.OnCompleted(() =>
            {
                _stopwatch.Stop();
                responseData.Add("ElapsedTime", _stopwatch.ElapsedMilliseconds + "ms");
                _logger.LogDebug(eventId, "[{RequestId}]返回结果=>{Message}", requestId, JsonUtil.Serialize(responseData, _jsonSerializerOptions));
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
                var result = ApiResult.Error((int)HttpStatusCode.InternalServerError, ex.Message);
                if (ex is ApiException ae)
                {
                    result.Code = ae.Code;
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
