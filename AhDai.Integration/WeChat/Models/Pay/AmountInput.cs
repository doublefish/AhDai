using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// 订单金额信息
/// </summary>
public class AmountInput
{
    /// <summary>
    /// 订单总金额：单位为分
    /// </summary>
    [Required]
    [JsonPropertyName("total")]
    public int Total { get; set; }
    /// <summary>
    /// 币种
    /// </summary>
    [Required]
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "CNY";

}
