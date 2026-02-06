using AhDai.Base.Extensions;
using AhDai.Base.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AhDai.Core.Utils;

/// <summary>
/// HttpHelper
/// </summary>
public static partial class HttpUtil
{

    [GeneratedRegex(@"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.IgnoreCase, "zh-CN")]
    public static partial Regex IpRegex();

    /// <summary>
    /// Client
    /// </summary>
    public static HttpClient Client { get; private set; }

    static HttpUtil()
    {
        var handler = new SocketsHttpHandler()
        {
            UseProxy = false,
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 50,
            // 每个请求连接的最大数量，默认是int.MaxValue
            MaxConnectionsPerServer = 100,
            // 连接池中TCP连接最多可以闲置多久，默认2分钟
            //PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
            // 连接最长的存活时间，默认是不限制的，一般不用设置
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            // 响应头最大字节数，单位: KB，默认64
            //MaxResponseHeadersLength = 64, 
            //是否自动处理cookie
            UseCookies = false,
        };
        Client = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(60)
        };
    }

    /// <summary>
    /// 创建请求
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static HttpRequestMessage CreateRequest(Models.HttpRequest data)
    {
        if (data.ContentType == HttpContentType.Json)
        {
            if (data.Body != null)
            {
                data.Content = JsonUtil.Serialize(data.Body);
            }
        }

        if (data.Method == HttpMethod.Get)
        {
            if (data.Query != null)
            {
                data.Url += (data.Url.Contains('?') ? '&' : '?') + data.Query.ToQueryString();
            }
        }

        var requestMessage = new HttpRequestMessage(data.Method, data.Url);
        if (data.Headers != null)
        {
            foreach (var kv in data.Headers)
            {
                requestMessage.Headers.Add(kv.Key, kv.Value);
            }
        }
        if (!requestMessage.Headers.Contains("Accept"))
        {
            requestMessage.Headers.Add("Accept", "*/*");
        }
        if (!requestMessage.Headers.Contains("Accept-Language"))
        {
            requestMessage.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,zh-TW;q=0.8,en-US;q=0.7,en;q=0.6");
        }
        if (!requestMessage.Headers.Contains("User-Agent"))
        {
            requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36 Edg/118.0.2088.76");
        }
        if (!requestMessage.Headers.Contains("Connection"))
        {
            requestMessage.Headers.Add("Connection", "keep-alive");
        }

        //async
        //写入请求参数
        if (!string.IsNullOrEmpty(data.Content))
        {
            var bytes = Encoding.UTF8.GetBytes(data.Content);
            requestMessage.Content = new ByteArrayContent(bytes);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(data.ContentType) { CharSet = "utf-8" };
            //requestMessage.Data.Headers.ContentLength = bytes.Length;
            //requestMessage.Data.Headers.ContentType.CharSet = "utf-8";
        }

        return requestMessage;
    }

    /// <summary>
    /// 转换响应
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static async Task<Models.HttpResponse> ConvertResponseAsync(HttpResponseMessage response)
    {
        var result = new Models.HttpResponse()
        {
            ResponseUri = null,
            StatusCode = response.StatusCode,
            ReasonPhrase = response.ReasonPhrase
        };
        if (response.Content.Headers.ContentType != null)
        {
            result.ContentType = response.Content.Headers.ContentType;
            result.ContentLength = response.Content.Headers.ContentLength ?? 0;
            result.ContentEncoding = response.Content.Headers.ContentEncoding;
            result.ContentLanguage = response.Content.Headers.ContentLanguage;
        }

        var charSet = response.Content.Headers.ContentType?.CharSet;
        var encoding = TextUtil.GetEncoding(charSet ?? "");
        if (charSet == "gzip")
        {
            using var stream = await response.Content.ReadAsStreamAsync();
            using var gzStream = new GZipStream(stream, CompressionMode.Decompress);
            using var reader = new StreamReader(gzStream, encoding);
            result.Content = reader.ReadToEnd();
        }
        else
        {
            //using var reader = new StreamReader(stream, encoding);
            result.Content = await response.Content.ReadAsStringAsync();
        }
        return result;
    }

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<Models.HttpResponse> SendAsync(Models.HttpRequest data)
    {
        var request = CreateRequest(data);
        var response = await SendAsync(request);
        return await ConvertResponseAsync(response);
    }

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        using var response = await Client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception($"[{(int)response.StatusCode}]{content}");
        }
        return response;
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public static HttpMethod ConvertHttpMethod(string method)
    {
        return method.ToUpper() switch
        {
            "GET" => HttpMethod.Get,
            "POST" => HttpMethod.Post,
            "PUT" => HttpMethod.Put,
            "DELETE" => HttpMethod.Delete,
            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>
    /// ParseQueryString
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static IDictionary<string, string>? ParseQueryString(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return null;
        }
        var array = query.Split('&');
        var dic = new Dictionary<string, string>();
        foreach (var kv in array)
        {
            var _array = kv.Split('=');
            if (_array.Length > 2)
            {
                _array = [_array[0], string.Join("=", _array, 1, _array.Length - 1)];
            }
            else if (_array.Length != 2)
            {
                throw new ArgumentException("Parameter format error.");
            }
            dic.Add(_array[0], _array[1]);
        }
        return dic;
    }

    /// <summary>
    /// UrlEncode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static T UrlEncode<T>(T parameters) where T : IDictionary<string, string>
    {
        return UrlEncode(parameters, Encoding.UTF8);
    }

    /// <summary>
    /// UrlEncode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static T UrlEncode<T>(T parameters, Encoding encoding) where T : IDictionary<string, string>
    {
        for (var i = 0; i < parameters.Keys.Count; i++)
        {
            var kv = parameters.ElementAt(i);
            parameters[kv.Key] = HttpUtility.UrlEncode(kv.Value, encoding);
        }
        return parameters;
    }

    /// <summary>
    /// UrlEncode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static T UrlEncodeUpper<T>(T parameters) where T : IDictionary<string, string>
    {
        return UrlEncodeUpper(parameters, Encoding.UTF8);
    }

    /// <summary>
    /// UrlEncode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static T UrlEncodeUpper<T>(T parameters, Encoding encoding) where T : IDictionary<string, string>
    {
        for (var i = 0; i < parameters.Keys.Count; i++)
        {
            var kv = parameters.ElementAt(i);
            parameters[kv.Key] = UrlEncodeUpper(kv.Value, encoding);
        }
        return parameters;
    }

    /// <summary>
    /// 转换Url编码为大写
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string UrlEncodeUpper(string str)
    {
        return UrlEncodeUpper(str, Encoding.UTF8);
    }

    /// <summary>
    /// 转换Url编码为大写
    /// </summary>
    /// <param name="str"></param>
    /// <param name="encoding">编码</param>
    /// <returns></returns>
    public static string UrlEncodeUpper(string str, Encoding encoding)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        var builder = new StringBuilder();
        foreach (var c in str)
        {
            var code = HttpUtility.UrlEncode(c.ToString(), encoding);
            if (code.Length > 1)
            {
                builder.Append(code.ToUpper());
            }
            else
            {
                builder.Append(c);
            }
        }
        return builder.ToString();
    }

    /// <summary>
    /// UrlDecode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static T UrlDecode<T>(T parameters) where T : IDictionary<string, string>
    {
        return UrlDecode(parameters, Encoding.UTF8);
    }

    /// <summary>
    /// UrlDecode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static T UrlDecode<T>(T parameters, Encoding encoding) where T : IDictionary<string, string>
    {
        for (var i = 0; i < parameters.Keys.Count; i++)
        {
            var kv = parameters.ElementAt(i);
            parameters[kv.Key] = HttpUtility.UrlDecode(kv.Value, encoding);
        }
        return parameters;
    }

    /// <summary>
    /// IsIp
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIp(string ip)
    {
        if (string.IsNullOrWhiteSpace(ip) || ip.Length < 7 || ip.Length > 15)
        {
            return false;
        }
        return IpRegex().IsMatch(ip);
    }
}
