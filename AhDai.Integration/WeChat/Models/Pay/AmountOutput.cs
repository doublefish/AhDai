using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// 订单金额信息
/// </summary>
public class AmountOutput
{
    /// <summary>
    /// 订单总金额，单位为分
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
    /// <summary>
    /// 用户支付金额，单位为分
    /// </summary>
    [JsonPropertyName("payer_total")]
    public int PayerTotal { get; set; }
    /// <summary>
    /// 币种
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;
    /// <summary>
    /// 用户支付币种
    /// </summary>
    [JsonPropertyName("payer_currency")]
    public string PayerCurrency { get; set; } = default!;
}
