using AhDai.Integration.Abstractions;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AhDai.Integration.Utils;

/// <summary>
/// HttpUtils
/// </summary>
internal class HttpUtils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="client"></param>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<TOutput> SendAsync<TOutput>(HttpClient client, HttpMethod method, string url, HttpContent? content, string serviceName)
        where TOutput : IBaseOutput
    {
        var request = new HttpRequestMessage(method, url)
        {
            Content = content,
        };
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<TOutput>() ?? throw new Exception($"请求{serviceName}服务发生异常：解析响应结果失败，请联系管理员");
        res.EnsureResult();
        return res;
    }
}
