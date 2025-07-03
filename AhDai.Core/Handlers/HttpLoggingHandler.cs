using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Handlers;

/// <summary>
/// HttpLoggingHandler
/// </summary>
/// <param name="logger"></param>
public class HttpLoggingHandler(ILogger<HttpLoggingHandler> logger) : DelegatingHandler
{
    private readonly ILogger<HttpLoggingHandler> _logger = logger;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestContent = "";
        if (request.Content != null && (request.Content.Headers.ContentType?.MediaType == "application/json"
            || request.Content.Headers.ContentType?.MediaType == "application/x-www-form-urlencoded"))
        {
            requestContent = await request.Content.ReadAsStringAsync(cancellationToken);
        }
        _logger.LogInformation("发送请求=>{Method} {RequestUri}\r\n{Content}", request.Method, request.RequestUri, requestContent);
        var watcher = new Stopwatch();
        watcher.Start();
        var response = await base.SendAsync(request, cancellationToken);
        watcher.Stop();
        var responseContent = "";
        if (response.Content != null && response.Content.Headers.ContentType?.MediaType == "application/json")
        {
            responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        }
        _logger.LogInformation("接收响应=>{StatusCode} {Content} 耗时{ElapsedMilliseconds}ms", response.StatusCode, responseContent, watcher.ElapsedMilliseconds);
        return response;
    }
}
