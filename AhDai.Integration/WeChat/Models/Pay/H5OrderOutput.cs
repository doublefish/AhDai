using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// H5下单
/// </summary>
public class H5OrderOutput : BaseOutput
{
    /// <summary>
    /// 支付跳转链接
    /// </summary>
    [JsonPropertyName("h5_url")]
    public string H5Url { get; set; } = default!;
}
