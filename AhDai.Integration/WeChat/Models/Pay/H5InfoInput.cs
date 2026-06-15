using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// H5场景信息
/// </summary>
public class H5InfoInput
{
    /// <summary>
    /// 场景类型
    /// </summary>
    [Required]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// 应用名称
    /// </summary>
    [JsonPropertyName("app_name")]
    public string? AppName { get; set; }
    /// <summary>
    /// 网站URL
    /// </summary>
    [JsonPropertyName("app_url")]
    public string? AppUrl { get; set; }
    /// <summary>
    /// iOS平台BundleID
    /// </summary>
    [JsonPropertyName("bundle_id")]
    public string? BundleID { get; set; }
    /// <summary>
    /// Android平台PackageName
    /// </summary>
    [JsonPropertyName("package_name")]
    public string? PackageName { get; set; }
}
