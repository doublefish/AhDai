using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// IpLocationInput
/// <see href="https://lbs.amap.com/api/webservice/guide/api/ipconfig">详细文档请参阅</see>
/// </summary>
public class IpLocationInput : BaseInput
{
    /// <summary>
    /// 需要搜索的IP地址（仅支持国内）
    /// 若用户不填写IP，则取客户HTTP之中的请求来进行定位
    /// </summary>
    [Required]
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }
}
