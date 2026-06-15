using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// DrivingLicenseInput
/// </summary>
public class DrivingLicenseInput : BaseInput
{
    /// <summary>
    /// - false：默认值，不检测朝向，朝向是指输入图像是正常方向、逆时针旋转90/180/270度
    /// - true：检测朝向
    /// </summary>
    [JsonPropertyName("detect_direction")]
    public string? DetectDirection { get; set; }
    /// <summary>
    /// - front：默认值，识别驾驶证正页、电子驾驶证正页
    /// - back：识别驾驶证副页
    /// </summary>
    [JsonPropertyName("driving_license_side")]
    public string? DrivingLicenseSide { get; set; }
    /// <summary>
    /// - false: 默认值，不进行归一化处理
    /// - true: 归一化格式输出，将驾驶证正页的「有效起始日期」+「有效期限」及「有效期限」+「至」，归一化为「有效起始日期」+「失效日期」格式输出
    /// </summary>
    [JsonPropertyName("unified_valid_period")]
    public string? UnifiedValidPeriod { get; set; }
    /// <summary>
    /// 是否开启质量检测功能，仅在驾驶证正页识别时生效，可选值如下
    /// - false：默认值，不输出质量告警信息
    /// - true：在 warn_infos 输出驾驶证遮挡、不完整、模糊质量告警信息。同时，可在 quality_propobility 输出质量检测置信度信息
    /// </summary>
    [JsonPropertyName("quality_warn")]
    public string? QualityWarn { get; set; }
    /// <summary>
    /// 是否开启风险检测功能，仅在驾驶证正页识别时生效，
    /// - false：默认值，不输出风险告警信息
    /// - true：开启，输出驾驶证复印、翻拍、PS等告警信息
    /// </summary>
    [JsonPropertyName("risk_warn")]
    public string? RiskWarn { get; set; }
}
