using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.WebApi.Configs;

/// <summary>
/// HttpLoggingMiddleware
/// </summary>
internal class HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
{
    readonly RequestDelegate _next = next;
    private readonly ILogger<HttpLoggingMiddleware> _logger = logger;

    /// <summary>
    /// InvokeAsync
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var builder = new StringBuilder();
        foreach (var header in context.Request.Headers)
        {
            builder.AppendFormat("{0}: {1}", header.Key, header.Value).AppendLine();
        }
        _logger.LogInformation("RequestHeaders:\n{Value}", builder.ToString());
        //using (_logger.BeginScope(new Dictionary<string, object> { { "TraceId", context.TraceIdentifier } }))
        //{
        //    await _next(context);
        //}
        await _next(context);
    }

}
