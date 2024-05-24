using AhDai.Base.Extensions;
using AhDai.Base.Utils;
using AhDai.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Versioning;

//[assembly: ResourceLocation("Resource Folder Name")]
//[assembly: RootNamespace("App Root Namespace")]

namespace AhDai.Core.Utils;

/// <summary>
/// VerifyCodeUtil
/// </summary>
public static class VerifyCodeUtil
{
    static readonly string[] Fonts = new string[] { "Helvetica", "Geneva", "sans-serif", "Verdana", "Times New Roman", "Courier New", "Arial" };
    static readonly Random Random;
    static readonly TimeSpan Expiry;
    static readonly string CacheKey = "VerifyCode";

    /// <summary>
    /// 构造函数
    /// </summary>
    static VerifyCodeUtil()
    {
        Random = new Random();
        Expiry = new TimeSpan(0, 1, 0);
    }

    /// <summary>
    /// 生成图片验证码
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    [SupportedOSPlatform("Windows")]
    public static byte[] GenerateImageCode(string guid, int length = 4)
    {
        if (string.IsNullOrEmpty(guid))
        {
            throw new ArgumentException("Unique code cannot be empty.");
        }
        var dateTime = DateTime.Now;
        var time = dateTime.TimeOfDay;
        var redisService = ServiceUtil.Services.GetRequiredService<IBaseRedisService>();
        var redis = redisService.GetDatabase(15);
        var key = $"{CacheKey}-{dateTime:yyMMddHH}";
        var hashField = guid;
        if (redis.HashExists(key, hashField))
        {
            throw new ArgumentException("Unique code already exists.");
        }
        var bytes = GenerateImageCode(length, out var code);
        var value = $"{code},{time.Add(Expiry)}";
        if (redis.KeyExists(key))
        {
            redis.HashSet(key, hashField, value);
        }
        else
        {
            redis.HashSet(key, hashField, value);
            redis.KeyExpire(key, new TimeSpan(1, 0, 0).Add(Expiry));
        }
        return bytes;
    }

    /// <summary>
    /// 验证图片验证码
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="code"></param>
    public static void VerifyImageCode(string? guid, string? code)
    {
        ArgumentException.ThrowIfNullOrEmpty(guid);
        ArgumentException.ThrowIfNullOrEmpty(code);
        var dateTime = DateTime.Now;
        var time = dateTime.TimeOfDay;
        var redisService = ServiceUtil.Services.GetRequiredService<IBaseRedisService>();
        var redis = redisService.GetDatabase(15);
        var key = $"{CacheKey}-{dateTime:yyMMddHH}";
        var hashField = guid;
        var value = redis.HashGet(key, hashField);
        if (value.IsNullOrEmpty && time.Minutes <= Expiry.TotalMinutes)
        {
            //从上一小时钟查询
            key = $"{CacheKey}-{dateTime.AddHours(-1):yyMMddHH}";
            value = redis.HashGet(key, hashField);
        }
        if (value.IsNullOrEmpty)
        {
            throw new ArgumentException("Verification code timeout.");
        }
        var array = value.ToString().Split(',');
        if (time > array[1].ToTimeSpan())
        {
            throw new ArgumentException("Verification code timeout.");
        }
        if (string.Compare(code, array[0], StringComparison.OrdinalIgnoreCase) != 0)
        {
            throw new ArgumentException("Verification code error.");
        }
        redis.HashDelete(key, hashField);
    }

    /// <summary>
    /// 生成指定长度的图片验证码
    /// </summary>
    /// <param name="length"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    [SupportedOSPlatform("Windows")]
    public static byte[] GenerateImageCode(int length, out string code)
    {
        code = StringUtil.GenerateRandom(length);
        var width = code.Length * 22;

        var bitmap = new Bitmap(width + 6, 38);
        var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(Color.White);

        for (var i = 0; i < 10; i++)
        {
            var x1 = Random.Next(bitmap.Width);
            var y1 = Random.Next(bitmap.Height);
            var x2 = Random.Next(bitmap.Width);
            var y2 = Random.Next(bitmap.Height);
            graphics.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        }

        graphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, bitmap.Width - 1, bitmap.Height - 1);

        for (var i = 0; i < code.Length; i++)
        {
            var x = new Matrix();
            x.Shear((float)Random.Next(0, 300) / 1000 - 0.25f, (float)Random.Next(0, 100) / 1000 - 0.05f);
            graphics.Transform = x;
            var str = code.Substring(i, 1);
            var brush = new LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            var point = new Point(i * 21 + 1 + Random.Next(3), 1 + Random.Next(13));
            var font = new Font(Fonts[Random.Next(Fonts.Length - 1)], Random.Next(14, 18), FontStyle.Bold);
            graphics.DrawString(str, font, brush, point);
        }

        for (var i = 0; i < 30; i++)
        {
            var x = Random.Next(bitmap.Width);
            var y = Random.Next(bitmap.Height);
            bitmap.SetPixel(x, y, Color.FromArgb(Random.Next()));
        }

        var ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Png);

        graphics.Dispose();
        bitmap.Dispose();

        return ms.ToArray();
    }

    #region Http
    /// <summary>
    /// 生成图片验证码
    /// </summary>
    /// <param name="httpResponse"></param>
    /// <param name="guid"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    [SupportedOSPlatform("Windows")]
    public static void GenerateImageCode(this HttpResponse httpResponse, string guid, int length = 4)
    {
        var bytes = GenerateImageCode(guid, length);
        httpResponse.Headers.Append("Access-Control-Expose-Headers", "X-VGuid");
        httpResponse.Headers.Append("X-VGuid", guid);
        httpResponse.ContentType = "image/png";
        httpResponse.StatusCode = StatusCodes.Status200OK;
        httpResponse.Body.WriteAsync(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// 验证图片验证码
    /// </summary>
    /// <param name="httpRequest"></param>
    public static void VerifyImageCode(this HttpRequest httpRequest)
    {
        httpRequest.Headers.TryGetValue("X-VGuid", out var guid);
        httpRequest.Headers.TryGetValue("X-VCode", out var code);

        try
        {
            VerifyImageCode(guid, code);
        }
        catch (ArgumentException ex)
        {
            var error = ex.Message switch
            {
                "Unique code cannot be empty." => "唯一编码已存在。",
                "Verification code error." => "验证码错误。",
                "Verification code timeout." => "验证码超时。",
                _ => ex.Message,
            };
            throw new ArgumentException(error);
        }
    }
    #endregion
}
