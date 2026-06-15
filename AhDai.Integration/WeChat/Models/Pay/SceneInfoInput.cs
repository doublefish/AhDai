using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// 支付场景信息
/// </summary>
public class SceneInfoInput
{
    /// <summary>
    /// 用户终端IP
    /// </summary>
    [Required]
    [JsonPropertyName("payer_client_ip")]
    public string PayerClientIp { get; set; } = default!;
    /// <summary>
    /// 商户端设备号
    /// </summary>
    [JsonPropertyName("device_id")]
    public string? DeviceId { get; set; }
    /// <summary>
    /// 商户门店信息
    /// </summary>
    [JsonPropertyName("store_info")]
    public StoreInfoInput? StoreInfo { get; set; }
    /// <summary>
    /// H5场景信息：H5支付必填
    /// </summary>
    [JsonPropertyName("h5_info")]
    public H5InfoInput? H5Info { get; set; }
}
