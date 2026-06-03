using AhDai.Core.Extensions;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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
    [GeneratedRegex(@"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase)]
    public static partial Regex IPv4Regex();

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
            MaxConnectionsPerServer = 200,
            // 连接池中TCP连接最多可以闲置多久，默认2分钟
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
            // 连接最长的存活时间，默认是不限制的，一般不用设置
            PooledConnectionLifetime = TimeSpan.FromMinutes(15),
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
        if (data.ContentType == HttpContentType.Json && data.Body != null)
        {
            data.Content = JsonUtil.Serialize(data.Body);
        }

        if (data.Method == HttpMethod.Get && data.Query != null)
        {
            var queryString = data.Query.ToQueryString();
            if (!string.IsNullOrEmpty(queryString))
            {
                data.Url += (data.Url.AsSpan().Contains('?') ? "&" : "?") + queryString;
            }
        }

        var requestMessage = new HttpRequestMessage(data.Method, data.Url);

        if (data.Headers != null)
        {
            foreach (var kv in data.Headers)
            {
                requestMessage.Headers.TryAddWithoutValidation(kv.Key, kv.Value);
            }
        }

        var headers = requestMessage.Headers;
        if (!headers.Contains("Accept")) headers.TryAddWithoutValidation("Accept", "*/*");
        if (!headers.Contains("Accept-Language")) headers.TryAddWithoutValidation("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
        if (!headers.Contains("User-Agent")) headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) HttpClient/8.0");
        if (!headers.Contains("Connection")) headers.TryAddWithoutValidation("Connection", "keep-alive");

        if (!string.IsNullOrEmpty(data.Content))
        {
            // 直接基于字符串构建 StringContent，由框架自动处理底层的编码开销，减少 byte[] 分配
            requestMessage.Content = new StringContent(data.Content, Encoding.UTF8, data.ContentType);
        }

        return requestMessage;
    }

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<Models.HttpResponse> SendAsync(Models.HttpRequest data)
    {
        using var request = CreateRequest(data);
        using var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new HttpRequestException($"[{(int)response.StatusCode}] 请求失败，原因: {errorContent}", null, response.StatusCode);
        }

        var result = new Models.HttpResponse()
        {
            StatusCode = response.StatusCode,
            ReasonPhrase = response.ReasonPhrase
        };

        if (response.Content.Headers.ContentType != null)
        {
            result.ContentType = response.Content.Headers.ContentType;
            result.ContentLength = response.Content.Headers.ContentLength ?? 0;
            result.ContentEncoding = response.Content.Headers.ContentEncoding;
        }

        var charSet = response.Content.Headers.ContentType?.CharSet;
        var encoding = TextUtil.GetEncoding(charSet ?? "utf-8");

        // 无论是常规响应还是 GZip 响应，全链路纯流式 StreamReader 读取
        using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        if (response.Content.Headers.ContentEncoding.Contains("gzip") || charSet == "gzip")
        {
            using var gzStream = new GZipStream(responseStream, CompressionMode.Decompress);
            using var reader = new StreamReader(gzStream, encoding);
            result.Content = await reader.ReadToEndAsync().ConfigureAwait(false);
        }
        else
        {
            using var reader = new StreamReader(responseStream, encoding);
            result.Content = await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        return result;
    }

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            using (response)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new HttpRequestException($"[{(int)response.StatusCode}]{content}", null, response.StatusCode);
            }
        }
        return response;
    }

    /// <summary>
    /// 将 URL 编码中的百分号十六进制字符完美转大写
    /// </summary>
    /// <param name="str"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string UrlEncodeUpper(string str, Encoding? encoding = null)
    {
        if (string.IsNullOrEmpty(str)) return str;
        encoding ??= Encoding.UTF8;

        // 1. 先通过微软官方标准转码
        var encoded = HttpUtility.UrlEncode(str, encoding);
        if (string.IsNullOrEmpty(encoded)) return encoded;

        // 直接在线扫描清洗，如果是百分号 '%', 将其后两位的小写字母转大写。
        // 在 Span 上就地修改，完美消灭 O(N²) 的原有 StringBuilder.Append(code.ToUpper()) 分配
        ReadOnlySpan<char> sourceSpan = encoded.AsSpan();
        if (!encoded.Contains('%')) return encoded;

        Span<char> destSpan = encoded.Length <= 512 ? stackalloc char[encoded.Length] : new char[encoded.Length];
        sourceSpan.CopyTo(destSpan);

        for (var i = 0; i < destSpan.Length - 2; i++)
        {
            if (destSpan[i] == '%')
            {
                // 小写字母 a-f 的 ASCII 码在 97~102 之间，减去 32 直接转大写
                ref var c1 = ref destSpan[i + 1];
                if (c1 is >= 'a' and <= 'f') c1 = (char)(c1 - 32);

                ref var c2 = ref destSpan[i + 2];
                if (c2 is >= 'a' and <= 'f') c2 = (char)(c2 - 32);

                i += 2; // 跳过处理完的十六进制位
            }
        }

        return destSpan.ToString();
    }

    /// <summary>
    /// 严谨的 IP 状态机判定（完美兼容 IPv4 与 现代云原生 IPv6 体系）
    /// </summary>
    public static bool IsIp(string ip)
    {
        if (string.IsNullOrWhiteSpace(ip)) return false;

        // 优先使用 .NET 原生高度优化的 IPAddress 状态机指针解析，速度比任何正则快 10 倍
        if (IPAddress.TryParse(ip, out _)) return true;

        // 针对特殊残缺格式走严谨正则判定
        return IPv4Regex().IsMatch(ip);
    }

    /// <summary>
    /// 是否内部私有网段 IP（全兼容双栈版）
    /// </summary>
    public static bool IsInternalIp(string ip)
    {
        if (!IPAddress.TryParse(ip, out var address)) return false;

        if (address.IsIPv4MappedToIPv6)
        {
            address = address.MapToIPv4();
        }

        if (IPAddress.IsLoopback(address)) return true;

        // 额外兼容 .NET 8/10 现代 IPv6 本地链路与私有局域网网段 (fe80::, fc00::)
        if (address.AddressFamily == AddressFamily.InterNetworkV6)
        {
            return address.IsIPv6LinkLocal || address.IsIPv6UniqueLocal;
        }

        if (address.AddressFamily == AddressFamily.InterNetwork)
        {
            var bytes = address.GetAddressBytes();
            if (bytes[0] == 10) return true;
            if (bytes[0] == 172 && bytes[1] is >= 16 and <= 31) return true;
            if (bytes[0] == 192 && bytes[1] == 168) return true;
        }

        return false;
    }
}
