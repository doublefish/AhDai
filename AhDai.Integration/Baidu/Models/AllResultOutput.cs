using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 核验结果及计费信息
/// </summary>
public class AllResultOutput
{
    /// <summary>
    /// 当前verify_token对应的核验次数，核验次数为几，后面就有几个对象数组 如方案配置为“认证未通过时URL失效”，则该verify_token核验次数为1； 如方案配置为“认证未通过时URL不失效”，则该verify_token对应的核验次数可能大于1（用户核验失败后多次重试）
    /// </summary>
    [JsonPropertyName("verify_count")]
    public int VerifyCount { get; set; }
    /// <summary>
    /// 身份证识别OCR接口的计费次数
    /// </summary>
    [JsonPropertyName("ocr_charge_count")]
    public int OcrChargeCount { get; set; }
    /// <summary>
    /// 人脸对比V4接口的计费次数 如方案配置为“认证未通过时URL失效”，计费次数最多为1； 如方案配置为“认证未通过时URL不失效”，则计费次数可能大于1（用户核验失败后多次重试）；
    /// </summary>
    [JsonPropertyName("match_charge_count")]
    public int MatchChargeCount { get; set; }
    /// <summary>
    /// 人脸实名认证V4接口的计费次数 如方案配置为“认证未通过时URL失效”，计费次数最多为1； 如方案配置为“认证未通过时URL不失效”，则计费次数可能大于1（用户核验失败后多次重试）；
    /// </summary>
    [JsonPropertyName("verify_charge_count")]
    public int VerifyChargeCount { get; set; }
    /// <summary>
    /// Live ? 接口的计费次数
    /// </summary>
    [JsonPropertyName("live_charge_count")]
    public int LiveChargeCount { get; set; }
    /// <summary>
    /// 核验结果信息，verify_count的值为几，就返回几个该对象
    /// </summary>
    [JsonPropertyName("verify_result")]
    public AllVerifyResult[] VerifyResult { get; set; } = default!;
    /// <summary>
    /// WillVerify
    /// </summary>
    [JsonPropertyName("will_verify")]
    public object[]? WillVerify { get; set; }
}
