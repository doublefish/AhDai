namespace AhDai.Integration.Baidu.Configs;

/// <summary>
/// BaiduFaceprintConfig
/// </summary>
public class BaiduFaceprintConfig : BaseBaiduConfig
{
    /// <summary>
    /// 人脸识别方案Id
    /// </summary>
    public string FaceprintPlanId { get; set; } = default!;
    /// <summary>
    /// 人脸识别Url
    /// </summary>
    public string FaceprintUrl { get; set; } = default!;
}
