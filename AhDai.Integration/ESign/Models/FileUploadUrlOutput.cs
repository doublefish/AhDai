using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 获取文件上传地址出参
/// </summary>
public class FileUploadUrlOutput
{
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonPropertyName("fileId")]
    public string FileId { get; set; } = default!;
    /// <summary>
    /// 文件上传地址，链接有效期60分钟
    /// </summary>
    [JsonPropertyName("fileUploadUrl")]
    public string FileUploadUrl { get; set; } = default!;
}
