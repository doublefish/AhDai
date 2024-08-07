﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AhDai.Core.Extensions;

/// <summary>
/// HttpRequestExtensions
/// </summary>
public static partial class HttpRequestExtensions
{
    /// <summary>
    /// MobileDeviceRegex
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"(iemobile|iphone|ipod|android|nokia|sonyericsson|blackberry|samsung|sec\-|windows ce|motorola|mot\-|up.b|midp\-)", RegexOptions.IgnoreCase | RegexOptions.Compiled, "zh-CN")]
    public static partial Regex MobileDeviceRegex();

    /// <summary>
    /// GetFullPath
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static string GetFullPath(this HttpRequest httpRequest)
    {
        return $"{httpRequest.Scheme}://{httpRequest.Host.Value}{httpRequest.PathBase.Value}";
    }

    /// <summary>
    /// 获取请求路径
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static string GetPath(this HttpRequest httpRequest)
    {
        var path = httpRequest.Path.Value ?? "";
        if (path.EndsWith("/"))
        {
            path = path[1..^1];
        }
        else
        {
            path = path[1..];
        }
        var routeValueCount = httpRequest.HttpContext.GetRouteData().Values.Keys.Count;
        if (routeValueCount > 2)
        {
            //移除路由参数
            var paths = path.Split('/');
            path = string.Join('/', paths, 0, paths.Length - routeValueCount + 2);
        }
        return path;
    }

    /// <summary>
    /// 验证请求频率
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="limit"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static void VerifyRequestFrequencyLimit(this HttpRequest httpRequest, double limit = 1D, string? path = null)
    {
        if (string.IsNullOrEmpty(path))
        {
            path = httpRequest.GetPath();
        }
        var ipAddress = httpRequest.HttpContext.Connection.RemoteIpAddress;
        try
        {
            Utils.HttpRequestUtil.VerifyFrequencyLimit(ipAddress?.ToString() ?? "", path, limit);
        }
        catch (Exception ex)
        {
            var error = ex.Message switch
            {
                "Requests are too frequent." => "请求过于频繁。",
                _ => ex.Message,
            };
            throw new ArgumentException(error);
        }
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static string GetParameters(this HttpRequest httpRequest)
    {
        var encoding = Encoding.UTF8;
        var contentType = httpRequest.ContentType?.Split(";")[0];
        return contentType switch
        {
            HttpContentType.Url => httpRequest.Query.ToQueryString(),
            HttpContentType.FormData => httpRequest.Form.ToQueryString(),
            _ => httpRequest.ReadBody(encoding),
        };
    }

    /// <summary>
    /// 读取内容
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string ReadBody(this HttpRequest httpRequest, Encoding? encoding = null)
    {
        var task = httpRequest.ReadBodyAsync(encoding);
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// 读取内容
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static async Task<string> ReadBodyAsync(this HttpRequest httpRequest, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        httpRequest.EnableBuffering();
        httpRequest.Body.Seek(0L, SeekOrigin.Begin);
        using var reader = new StreamReader(httpRequest.Body, encoding);
        var content = await reader.ReadToEndAsync();
        httpRequest.Body.Position = 0L;
        return content;
    }

    /// <summary>
    /// 是否移动设备
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static bool IsMobileDevice(this HttpRequest httpRequest)
    {
        httpRequest.Headers.TryGetValue("User-Agent", out var values);
        var userAgent = values.ToString();
        if (string.IsNullOrEmpty(userAgent))
        {
            return false;
        }
        return MobileDeviceRegex().IsMatch(userAgent);
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="headers"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string? GetValue(this IHeaderDictionary headers, string key)
    {
        headers.TryGetValue(key, out var values);
        return values.FirstOrDefault();
    }


}
