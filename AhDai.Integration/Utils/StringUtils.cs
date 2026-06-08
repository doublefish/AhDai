using AhDai.Core.Metadata;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Integration.Utils;

/// <summary>
/// StringUtils
/// </summary>
public class StringUtils
{
    /// <summary>
    /// ConvertToBase64Async
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static async Task<string> ConvertToBase64Async(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
        return Convert.ToBase64String(fileBytes);
    }

    /// <summary>
    /// ConvertToHexString
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ConvertToHexString(byte[] bytes)
    {
#if NET9_0_OR_GREATER
        return Convert.ToHexStringLower(bytes);
#else
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
#endif
    }

    /// <summary>
    /// 生成随机字符串
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GenerateRandomString(int length)
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes)[..length];
    }

    /// <summary>
    /// ToQueryString
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="ignoreNullOrEmpty"></param>
    /// <param name="escape"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static string ToQueryString(IDictionary<string, string?>? dict, bool ignoreNullOrEmpty = false, bool escape = false, bool percent = false)
    {
        if (dict == null || dict.Count == 0) return "";

        var builder = new StringBuilder();
        foreach (var kvp in dict)
        {
            AppendParameter(builder, kvp.Key, kvp.Value, ignoreNullOrEmpty, escape, percent);
        }
        return builder.ToString();
    }

    /// <summary>
    /// ToQueryString
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="ignoreNullOrEmpty"></param>
    /// <param name="escape"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static string ObjectToQueryString<T>(T obj, bool ignoreNullOrEmpty = false, bool escape = false, bool percent = false) where T : class
    {
        if (obj == null) return "";

        var props = TypeMetadataProvider.GetProperties<T>();
        var builder = new StringBuilder();
        foreach (var p in props)
        {
            var key = p.JsonName ?? p.Info.Name;
            var val = p.Info.GetValue(obj)?.ToString();
            AppendParameter(builder, key, val, ignoreNullOrEmpty, escape, percent);
        }
        return builder.ToString();
    }

    static StringBuilder AppendParameter(StringBuilder builder, string key, string? value, bool ignoreNullOrEmpty, bool escape, bool percent)
    {
        var v = value ?? "";
        if (ignoreNullOrEmpty && string.IsNullOrEmpty(v)) return builder;

        if (builder.Length > 0) builder.Append('&');

        var k = key;
        if (escape)
        {
            k = Uri.EscapeDataString(k);
            v = Uri.EscapeDataString(v);
        }

        if (percent)
        {
            k = PercentCode(k);
            v = PercentCode(v);
        }

        builder.Append(k).Append('=').Append(v);

        return builder;
    }

    /// <summary>
    /// PercentCode
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string PercentCode(string str)
    {
        if (str.Contains('+') || str.Contains('*') || str.Contains("%7E"))
        {
            Console.WriteLine("PercentCode: {0}", str);
        }
        return str.Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
    }
}
