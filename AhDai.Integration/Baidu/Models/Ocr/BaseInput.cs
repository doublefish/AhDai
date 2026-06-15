using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// BaseInput
/// </summary>
public abstract class BaseInput
{
    /// <summary>
    /// 图像数据，base64编码后进行urlencode，要求base64编码和urlencode后大小不超过4M，最短边至少15px，最长边最大4096px，支持jpg/jpeg/png/bmp格式
    /// 优先级：image > url > pdf_file > ofd_file ，当image字段存在时，url、pdf_file、ofd_file 字段失效
    /// </summary>
    [JsonPropertyName("image")]
    public virtual string? Image { get; set; }
    /// <summary>
    /// 图片完整url，url长度不超过1024字节，url对应的图片base64编码后大小不超过4M，最短边至少15px，最长边最大4096px，支持jpg/jpeg/png/bmp格式
    /// 优先级：image > url > pdf_file > ofd_file，当image字段存在时，url字段失效
    /// 请注意关闭URL防盗链
    /// </summary>
    [JsonPropertyName("url")]
    public virtual string? Url { get; set; }

    /// <summary>
    /// 设置文件内容
    /// </summary>
    /// <param name="fileString"></param>
    /// <param name="fileType">1-Image</param>
    public virtual void SetFile(string fileString, int fileType)
    {
        if (fileType != 1) throw new ArgumentException($"无法识别文件类型：{fileType}");
        Image = fileString;
    }

    /// <summary>
    /// 文件
    /// </summary>
    [JsonIgnore]
    public virtual IFormFile? File { get; set; }
}
