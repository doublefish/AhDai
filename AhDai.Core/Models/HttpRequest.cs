using System.Collections.Generic;
using System.Net.Http;

namespace AhDai.Core.Models;

/// <summary>
/// Http请求数据
/// </summary>
/// <param name="method"></param>
/// <param name="url"></param>
/// <param name="contentType"></param>
public class HttpRequest(HttpMethod method, string url, string contentType = HttpContentType.Url)
{
    /// <summary>
    /// 类型
    /// </summary>
    public HttpMethod Method { get; set; } = method;
    /// <summary>
    /// 地址
    /// </summary>
    public string Url { get; set; } = url;
    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; } = contentType;
    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// Query
    /// </summary>
    public IDictionary<string, object>? Query { get; set; }
    /// <summary>
    /// 头
    /// </summary>
    public IDictionary<string, string>? Headers { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public IDictionary<string, object>? Body { get; set; }
}
