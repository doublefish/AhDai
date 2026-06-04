using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 获取文件上传地址入参
/// </summary>
public class FileUploadUrlInput
{
    /// <summary>
    /// 文件的Content-MD5值。
    /// 先获取文件MD5的128位二进制数组，再对此二进制进行Base64编码。
    /// </summary>
    [JsonPropertyName("contentMd5")]
    public string ContentMd5 { get; set; } = default!;
    /// <summary>
    /// 目标文件的MIME类型
    /// 可填写 application/octet-stream 或 application/pdf
    /// </summary>
    [JsonPropertyName("contentType")]
    public string ContentType { get; set; } = default!;
    /// <summary>
    /// 文件名称（必须带上文件扩展名）
    /// </summary>
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = default!;
    /// <summary>
    /// 文件大小，单位: byte字节
    /// </summary>
    [JsonPropertyName("fileSize")]
    public long FileSize { get; set; }
    /// <summary>
    /// 是否需要转换成PDF文档，默认值 false
    /// </summary>
    [JsonPropertyName("convertToPDF")]
    public bool? ConvertToPDF { get; set; }
    /// <summary>
    /// 是否需要转换成HTML文档，默认值 false
    /// </summary>
    [JsonPropertyName("convertToHTML")]
    public bool? ConvertToHTML { get; set; }
    /// <summary>
    /// 专属云项目ID
    /// </summary>
    [JsonPropertyName("dedicatedCloudId")]
    public string? DedicatedCloudId { get; set; }
}
