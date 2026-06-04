using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// SignFileOutput
/// </summary>
public class SignFileOutput
{
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonPropertyName("fileId")]
    public string FileId { get; set; } = default!;
    /// <summary>
    /// 文件名称
    /// </summary>
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = default!;
    /// <summary>
    /// 文件下载地址：有效期60分钟
    /// </summary>
    [JsonPropertyName("downloadUrl")]
    public string DownloadUrl { get; set; } = default!;
}
