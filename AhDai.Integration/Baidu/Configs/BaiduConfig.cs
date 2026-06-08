using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.Baidu.Configs;

/// <summary>
/// BaiduConfig
/// </summary>
public class BaiduConfig : BaseConfig
{
    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// ApiKey
    /// </summary>
    public string ApiKey { get; set; } = default!;
    /// <summary>
    /// AppSecret
    /// </summary>
    [Sensitive]
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// 人脸识别方案Id
    /// </summary>
    public string FaceprintPlanId { get; set; } = default!;
    /// <summary>
    /// 人脸识别Url
    /// </summary>
    public string FaceprintUrl { get; set; } = default!;
}
