using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AhDai.Core.Utils;

/// <summary>
/// FileUtil
/// </summary>
public static class FileUtil
{
    static readonly Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider ContentTypeProvider = new();

    /// <summary>
    /// EnsureDirectoryExists
    /// </summary>
    /// <param name="directoryPath"></param>
    public static void EnsureDirectoryExists(string? directoryPath)
    {
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    /// <summary>
    /// EnsureParentDirectoryExists
    /// </summary>
    /// <param name="filePath"></param>
    public static void EnsureParentDirectoryExists(string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);
        EnsureDirectoryExists(dir);
    }

    /// <summary>
    /// 输出图片
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> OutputImageAsync(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);

        if (!ContentTypeProvider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(path)}";

        return Output(stream, contentType, fileName);
    }

    /// <summary>
    /// 输出文件
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static HttpResponseMessage Output(byte[] bytes, string type, string name)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(bytes)
        };

        SetResponseHeaders(response.Content, type, name);
        return response;
    }

    /// <summary>
    /// 输出文件
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static HttpResponseMessage Output(Stream stream, string type, string name)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(stream)
        };

        if (stream.CanSeek)
        {
            response.Content.Headers.ContentLength = stream.Length;
        }

        SetResponseHeaders(response.Content, type, name);
        return response;
    }

    /// <summary>
    /// 转换为物理路径
    /// </summary>
    /// <param name="path"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ToPhysicalPath(string path, char separator = '/')
    {
        if (string.IsNullOrWhiteSpace(path)) return string.Empty;

        if (Path.DirectorySeparatorChar != separator)
        {
            path = path.Replace(separator, Path.DirectorySeparatorChar);
        }

        return Path.GetFullPath(path);
    }

    /// <summary>
    /// 换算文件大小
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string GetFileSize(long bytes)
    {
        if (bytes <= 0) return "0 B";

        var units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };

        var digitGroups = (int)(Math.Log10(bytes) / Math.Log10(1024));

        // 防御上边界，防止超越单位数组
        if (digitGroups >= units.Length) digitGroups = units.Length - 1;

        var size = bytes / Math.Pow(1024, digitGroups);

        return $"{size:F2} {units[digitGroups]}";
    }

    static void SetResponseHeaders(HttpContent content, string type, string name)
    {
        content.Headers.ContentType = new MediaTypeHeaderValue(type);
        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = name
        };
    }
}
