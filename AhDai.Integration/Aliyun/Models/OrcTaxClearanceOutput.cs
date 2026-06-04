using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models;

/// <summary>
/// OrcTaxClearanceOutput
/// </summary>
public class OrcTaxClearanceOutput
{
    /// <summary>
    /// 填发日期
    /// </summary>
    [JsonPropertyName("issueDate")]
    public string? IssueDate { get; set; }
    /// <summary>
    /// 编号
    /// </summary>
    [JsonPropertyName("certificateNumber")]
    public string? CertificateNumber { get; set; }
    /// <summary>
    /// 税务机关
    /// </summary>
    [JsonPropertyName("taxAuthorityName")]
    public string? TaxAuthorityName { get; set; }
    /// <summary>
    /// 收据联
    /// </summary>
    [JsonPropertyName("formType")]
    public string? FormType { get; set; }
    /// <summary>
    /// 纳税人识别号
    /// </summary>
    [JsonPropertyName("taxNumbe")]
    public string? TaxNumber { get; set; }
    /// <summary>
    /// 纳税人名称
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// 金额合计大写
    /// </summary>
    [JsonPropertyName("totalAmountInWords")]
    public string? TotalAmountInWords { get; set; }
    /// <summary>
    /// 金额合计
    /// </summary>
    [JsonPropertyName("totalAmount")]
    public string? TotalAmount { get; set; }
    /// <summary>
    /// 填票人
    /// </summary>
    [JsonPropertyName("drawer")]
    public string? Drawer { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [JsonPropertyName("remarks")]
    public string? Remarks { get; set; }
    /// <summary>
    /// 明细
    /// </summary>
    [JsonPropertyName("taxClearanceDetails")]
    public OrcTaxClearanceDetailOutput[]? TaxClearanceDetails { get; set; }
}
