using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// IpLocationOutput
/// </summary>
public class IpLocationOutput : BaseMapOutput
{
    /// <summary>
    /// 详细地址信息
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// 内容
    /// </summary>
    [JsonPropertyName("content")]
    public IpLocationContentOutput Content { get; set; } = default!;
}
