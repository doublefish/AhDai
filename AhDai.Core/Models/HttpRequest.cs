using System.Collections.Generic;
using System.Net.Http;

namespace AhDai.Core.Models;

/// <summary>
/// Http请求数据
/// </summary>
public class HttpRequest
{
    /// <summary>
    /// 类型
    /// </summary>
    public HttpMethod Method { get; set; }
    /// <summary>
    /// 地址
    /// </summary>
    public string Url { get; set; } = null!;
    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; } = null!;
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

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="contentType"></param>
    public HttpRequest(HttpMethod method, string url, string contentType = HttpContentType.Url)
    {
        Method = method;
        Url = url;
        ContentType = contentType;
    }
}
