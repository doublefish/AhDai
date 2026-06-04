using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 采集的身份证图片信息
/// </summary>
public class IdCardImageOutput
{
    /// <summary>
    /// 身份证图片的正面信息
    /// </summary>
    [JsonPropertyName("front_base64")]
    public string FrontBase64 { get; set; } = default!;
    /// <summary>
    /// 身份证图片的反面信息
    /// 当人脸实名认证控制台设置为使用OCR识别且为国徽面+人像面时返回此参数信息
    /// </summary>
    [JsonPropertyName("back_base64")]
    public string BackBase64 { get; set; } = default!;
}
