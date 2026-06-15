using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// 支付结算信息
/// </summary>
public class SettleInfoInput
{
    /// <summary>
    /// 是否指定分账
    /// </summary>
    [JsonPropertyName("profit_sharing")]
    public bool? ProfitSharing { get; set; }
}
