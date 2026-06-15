using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// CertificateDetailGetOutput
/// </summary>
public class CertificateDetailGetOutput : BaseOutput
{
    /// <summary>
    /// 状态值
    /// </summary>
    [JsonPropertyName("code")]
    public long Code { get; set; }
    /// <summary>
    /// 状态信息描述
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;
    /// <summary>
    /// 存证证明下载地址
    /// </summary>
    [JsonPropertyName("url")]
    public string FileUrl { get; set; } = default!;
}
