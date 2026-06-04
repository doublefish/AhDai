using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 创建二维码入参
/// </summary>
public class QrCodeCreateInput
{
    /// <summary>
    /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天），此字段如果不填，则默认有效期为60秒
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("expire_seconds")]
    public int? ExpireSeconds { get; set; }
    /// <summary>
    /// 二维码类型，QR_SCENE为临时的整型参数值，QR_STR_SCENE为临时的字符串参数值，QR_LIMIT_SCENE为永久的整型参数值，QR_LIMIT_STR_SCENE为永久的字符串参数值
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("action_name")]
    public string ActionName { get; set; } = default!;
    /// <summary>
    /// 二维码详细信息
    /// </summary>
    [JsonPropertyName("action_info")]
    public QrCodeCreateActionInfoInput ActionInfo { get; set; } = default!;

}
