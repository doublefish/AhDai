using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// SignFileDownloadOutput
/// </summary>
public class SignFileDownloadOutput
{
    /// <summary>
    /// 签署文件信息
    /// </summary>
    [JsonPropertyName("files")]
    public SignFileOutput[] Files { get; set; } = default!;
    /// <summary>
    /// 附属材料信息
    /// </summary>
    [JsonPropertyName("attachments")]
    public SignFileOutput[]? Attachments { get; set; }
    /// <summary>
    /// 海外签证书报告下载地址：有效期60分钟
    /// </summary>
    [JsonPropertyName("certificateDownloadUrl")]
    public string? CertificateDownloadUrl { get; set; }

}
