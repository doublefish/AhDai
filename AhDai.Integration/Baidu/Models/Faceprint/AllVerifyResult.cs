using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Faceprint;

/// <summary>
/// 核验结果
/// </summary>
public class AllVerifyResult
{
    /// <summary>
    /// 代表本次核验结果在verify_token所有核验次数中的顺序，按时间先后排序，第一次核验返回值为 1，第二次核验返回值为 2，以此类推
    /// </summary>
    [JsonPropertyName("order")]
    public int Order { get; set; }
    /// <summary>
    /// 本次核验是否通过 核验成功返回true 核验失败返回false
    /// </summary>
    [JsonPropertyName("is_verify_passed")]
    public bool IsVerifyPassed { get; set; }
    /// <summary>
    /// 本次核验的错误码 核验成功时返回 0 核验失败返回非0错误码
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;
    /// <summary>
    /// 本次核验的错误信息 核验成功时返回“核验成功” 核验失败时返回具体错误原因，如“身份证姓名不匹配”
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;
    /// <summary>
    /// 本次核验完成的时间点，包含年月日、时分秒，如“2023-6-20 18:00:00”
    /// </summary>
    [JsonPropertyName("verify_time")]
    public DateTime VerifyTime { get; set; }
    /// <summary>
    /// 本次核验是否计费 计费返回true 不计费返回false
    /// </summary>
    [JsonPropertyName("is_charged")]
    public bool IsCharged { get; set; }
    /// <summary>
    /// 计费类型：verify(人脸实名认证V4)、match(人脸对比V4)
    /// </summary>
    [JsonPropertyName("charge_type")]
    public string ChargeType { get; set; } = default!;
    /// <summary>
    /// 本次计费的时间点，如不计费则该字段为空，包含年月日、时分秒，如“2023-6-20 18:00:00”
    /// </summary>
    [JsonPropertyName("charge_time")]
    public DateTime ChargeTime { get; set; }
    /// <summary>
    /// 身份证识别OCR是否计费 计费返回true 不计费返回false
    /// </summary>
    [JsonPropertyName("is_ocr_charge")]
    public bool IsOcrCharge { get; set; }
    /// <summary>
    /// ocr 收费时间点，如不收费则该字段为空，包含年月日、时分秒，如“2023-6-20 18:00:00”
    /// </summary>
    [JsonPropertyName("ocr_charge_time")]
    public DateTime? OcrChargeTime { get; set; }
    /// <summary>
    /// ocr次数
    /// </summary>
    [JsonPropertyName("ocr_count")]
    public int OcrCount { get; set; }
    /// <summary>
    /// 核验的详细信息
    /// </summary>
    [JsonPropertyName("verify_detail")]
    public VerifyDetail VerifyDetail { get; set; } = default!;
}
