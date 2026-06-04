using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrIdCardInput
/// </summary>
public class OcrIdCardInput : BaseOcrInput
{
    /// <summary>
    /// - front：身份证含照片的一面
    /// - back：身份证带国徽的一面
    /// 自动检测身份证正反面，如果传参指定方向与图片相反，支持正常识别，返回参数image_status字段为"reversed_side"
    /// </summary>
    [JsonPropertyName("id_card_side")]
    public string IdCardSide { get; set; } = default!;
    /// <summary>
    /// 是否检测上传的身份证被PS，默认不检测。可选值：-true：检测；- false：不检测
    /// </summary>
    [JsonPropertyName("detect_ps")]
    public string? DetectPs { get; set; }
    /// <summary>
    /// 是否开启身份证风险类型（身份证复印件/扫描件、临时身份证、身份证翻拍/截屏、修改过的身份证）检测功能，默认不开启，即：false。
    /// - true：开启，请查看返回参数risk_type；
    /// - false：不开启
    /// </summary>
    [JsonPropertyName("detect_risk")]
    public string? DetectRisk { get; set; }
    /// <summary>
    /// 是否开启身份证质量类型（清晰模糊、边框/四角不完整、头像或关键字段被遮挡/马赛克）检测功能，默认不开启，即：false。
    /// - true：开启，请查看返回参数card_quality；
    /// - false：不开启
    /// </summary>
    [JsonPropertyName("detect_quality")]
    public string? DetectQuality { get; set; }
    /// <summary>
    /// 是否检测头像内容，默认不检测，即：false。
    /// - true：检测头像并返回头像的 base64 编码及位置信息；
    /// - false：不检测
    /// </summary>
    [JsonPropertyName("detect_photo")]
    public string? DetectPhoto { get; set; }
    /// <summary>
    /// 是否检测身份证进行裁剪，默认不检测，即：false。
    /// - true：检测身份证并返回证照的 base64 编码及位置信息；
    /// - false：不检测
    /// </summary>
    [JsonPropertyName("detect_card")]
    public string? DetectCard { get; set; }
    /// <summary>
    /// 是否检测上传的身份证图片方向，默认不检测，即：false。
    /// - true：检测；
    /// - false：不检测
    /// </summary>
    [JsonPropertyName("detect_direction")]
    public string? DetectDirection { get; set; }
    /// <summary>
    /// 是否细分输出截屏风险类型，当 detect_risk =ture 时，该参数才生效。默认不开启，即：false。
    /// - true：开启，将在 risk_type 细分输出 screenshot 截屏类型；
    /// - false：不开启，risk_type 将合并 screenshot 截屏到 screen 翻拍类型输出
    /// </summary>
    [JsonPropertyName("detect_screenshot")]
    public string? DetectScreenshot { get; set; }
}
