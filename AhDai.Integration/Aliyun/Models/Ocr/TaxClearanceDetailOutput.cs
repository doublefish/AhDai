using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models.Ocr;

/// <summary>
/// TaxClearanceDetailOutput
/// </summary>
public class TaxClearanceDetailOutput
{
    /// <summary>
    /// 凭证号
    /// </summary>
    [JsonPropertyName("voucherNumber")]
    public string? VoucherNumber { get; set; }
    /// <summary>
    /// 税种
    /// </summary>
    [JsonPropertyName("taxType")]
    public string? TaxType { get; set; }
    /// <summary>
    /// 品目名称
    /// </summary>
    [JsonPropertyName("itemName")]
    public string? ItemName { get; set; }
    /// <summary>
    /// 税款所属时期
    /// </summary>
    [JsonPropertyName("taxPeriod")]
    public string? TaxPeriod { get; set; }
    /// <summary>
    /// 入（退）库日期
    /// </summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }
    /// <summary>
    /// 实缴（退）金额
    /// </summary>
    [JsonPropertyName("amount")]
    public string? Amount { get; set; }
}
