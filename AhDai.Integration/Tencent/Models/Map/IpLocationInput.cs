using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models.Map;

/// <summary>
/// IpLocationInput
/// <see href="https://lbs.qq.com/service/webService/webServiceGuide/position/webServiceIp">详细文档请参阅</see>
/// </summary>
public class IpLocationInput : BaseInput
{
    /// <summary>
    /// IP地址，缺省时会使用请求端的IP
    /// </summary>
    [Required]
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }
}
