using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 门店信息
/// </summary>
public class StoreInfoInput
{
    /// <summary>
    /// 门店编号
    /// </summary>
    [Required]
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    /// <summary>
    /// 门店名称
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// 地区编码
    /// </summary>
    [JsonPropertyName("area_code")]
    public string? AreaCode { get; set; }
    /// <summary>
    /// 详细地址
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }
}
