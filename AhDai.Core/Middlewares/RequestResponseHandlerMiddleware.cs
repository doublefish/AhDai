using AhDai.Base.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Core.Middlewares;

/// <summary>
/// RequestResponseHandlerMiddleware
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public class RequestResponseHandlerMiddleware(RequestDelegate next
    , ILogger<RequestResponseHandlerMiddleware> logger)
{
    readonly RequestDelegate _next = next;
    readonly ILogger<RequestResponseHandlerMiddleware> _logger = logger;

    readonly Stopwatch _stopwatch = new();

    /// <summary>
    /// InvokeAsync
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        _stopwatch.Restart();

        var eventId = new EventId(0, context.TraceIdentifier);

        // 记录请求
        context.Request.EnableBuffering();
        var requestHeaders = context.Request.Headers.ToDictionary(k => k.Key, v => string.Join(";", [.. v.Value]));
        var requestBody = "";
        if (context.Request.ContentType != null && !context.Request.ContentType.StartsWith("multipart/form-requestData")
           && !context.Request.ContentType.StartsWith("multipart/form-data; boundary="))
        {
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            using var requestReader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
            requestBody = await requestReader.ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
        }
        _logger.LogInformation(eventId, "接收请求=>{Method} {Path} {Headers} {Body}", context.Request.Method, context.Request.Path, requestHeaders, requestBody);

        // 拦截响应
        var originalResponseBodyStream = context.Response.Body;
        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(eventId, ex, "发生异常=>{Message}", ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = new ApiResult()
            {
                Code = context.Response.StatusCode,
                Message = ex.Message,
                TraceId = context.TraceIdentifier
            };
            await context.Response.WriteAsJsonAsync(result);
        }

        // 记录响应
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseReader = new StreamReader(context.Request.Body, Encoding.UTF8);
        var responseBody = await responseReader.ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        _stopwatch.Stop();
        _logger.LogInformation(eventId, "输出响应=>{StatusCode} {Body} - Processed in {ElapsedMilliseconds}ms ", context.Response.StatusCode, responseBody, _stopwatch.ElapsedMilliseconds);

        await context.Response.Body.CopyToAsync(originalResponseBodyStream);
    }


}
