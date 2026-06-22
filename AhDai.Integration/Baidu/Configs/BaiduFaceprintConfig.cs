namespace AhDai.Integration.Baidu.Configs;

/// <summary>
/// BaiduFaceprintConfig
/// </summary>
public class BaiduFaceprintConfig : BaseBaiduConfig
{
    /// <summary>
    /// 方案Id
    /// </summary>
    public string PlanId { get; set; } = default!;
    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; set; } = default!;
}
