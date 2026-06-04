using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrVehicleLicenseInput
/// </summary>
public class OcrVehicleLicenseInput : BaseOcrInput
{
    /// <summary>
    /// - false：默认值不进行图像方向自动矫正
    /// - true: 开启图像方向自动矫正功能，可对旋转 90/180/270 度的图片进行自动矫正并识别
    /// </summary>
    [JsonPropertyName("detect_direction")]
    public string? DetectDirection { get; set; }
    /// <summary>
    /// - front：默认值，识别行驶证主页、电子行驶证主页
    /// - back：识别行驶证副页、电子行驶证副页
    /// </summary>
    [JsonPropertyName("vehicle_license_side")]
    public string? VehicleLicenseSide { get; set; }
    /// <summary>
    /// - false：默认值，不进行归一化处理
    /// - true：对输出字段进行归一化处理，将新/老版行驶证的“注册登记日期/注册日期”统一为”注册日期“进行输出
    /// </summary>
    [JsonPropertyName("unified")]
    public string? Unified { get; set; }
    /// <summary>
    /// 是否开启质量检测功能，仅在行驶证正页识别时生效，可选值如下
    /// - false：默认值，不输出质量告警信息
    /// - true：在 warn_infos 输出行驶证遮挡、不完整、模糊质量告警信息。同时，可在 quality_propobility 输出质量检测置信度信息
    /// </summary>
    [JsonPropertyName("quality_warn")]
    public string? QualityWarn { get; set; }
    /// <summary>
    /// 是否开启风险检测功能，仅在行驶证正页识别时生效，
    /// - false：默认值，不输出风险告警信息
    /// - true：开启，输出行驶证复印、翻拍、PS等告警信息
    /// </summary>
    [JsonPropertyName("risk_warn")]
    public string? RiskWarn { get; set; }
}
