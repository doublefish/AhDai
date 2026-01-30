using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AhDai.Core.Utils;

/// <summary>
/// FileUtil
/// </summary>
public static class FileUtil
{
    /// <summary>
    /// 输出图片
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static HttpResponseMessage OutputImage(string path)
    {
        var task = Task.Run(() => { return OutputImageAsync(path); });
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// 输出图片
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> OutputImageAsync(string path)
    {
        if (!File.Exists(path))
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        var bytes = await File.ReadAllBytesAsync(path);
        var extension = Path.GetExtension(path);
        var type = $"image/{extension[1..]}";
        var name = $"{Guid.NewGuid()}{extension}";
        return Output(bytes, type, name);
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
        response.Content.Headers.ContentLength = bytes.Length;
        response.Content.Headers.ContentType = new MediaTypeHeaderValue(type);
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = HttpUtility.UrlEncode(name, Encoding.UTF8)
        };
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
        //response.Data.Headers.ContentLength = stream.Length;
        response.Content.Headers.ContentType = new MediaTypeHeaderValue(type);
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = HttpUtility.UrlEncode(name, Encoding.UTF8)
        };
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
        var paths = path.Split(separator);
        return Path.Combine(paths);
    }

    /// <summary>
    /// 换算文件大小
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string GetFileSize(long b)
    {
        var size = (double)b;
        var units = new string[] { "B", "KB", "MB", "GB", "TB" };
        var unit = units[0];
        var i = 0;
        while (size > 100 && i < units.Length)
        {
            size /= 1024;
            unit = units[i + 1];
            i++;
        }
        size = Math.Round(size, 2);
        return size + unit;
    }
}
