using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 认证结果
/// </summary>
public class DetailResultOutput
{
    /// <summary>
    /// 认证返还信息
    /// </summary>
    [JsonPropertyName("verify_result")]
    public VerifyResult VerifyResult { get; set; } = default!;
    /// <summary>
    /// 用户二次确认的身份证信息
    /// </summary>
    [JsonPropertyName("idcard_confirm")]
    public IdCardConfirmOutput IdCardConfirm { get; set; } = default!;
    /// <summary>
    /// 采集的身份证信息
    /// 当人脸实名认证控制台设置为使用OCR识别时返回此参数信息
    /// </summary>
    [JsonPropertyName("idcard_ocr_result")]
    public IdCardOcrResult? IdCardOcrResult { get; set; }
    /// <summary>
    /// 采集的身份证图片信息
    /// 当人脸实名认证控制台设置为使用OCR识别时返回此参数信息
    /// </summary>
    [JsonPropertyName("idcard_images")]
    public IdCardImageOutput? IdCardImages { get; set; }
    /// <summary>
    /// 方案配置为实时检测时，将返回该字段，代表本次核验是否出现降级
    /// 降级返回true，未降级返回false
    /// </summary>
    [JsonPropertyName("is_demote")]
    public bool? IsDemote { get; set; }
}
