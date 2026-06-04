using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 机构认证信息
/// </summary>
public class OrgAuthOutput
{
    /// <summary>
    /// 组织机构证件号
    /// </summary>
    [JsonPropertyName("orgIDCardNum")]
    public string? OrgIDCardNum { get; set; }
    /// <summary>
    /// 组织机构证件号类型
    /// CRED_ORG_USCC - 统一社会信用
    /// CRED_ORG_REGCODE - 工商注册号
    /// </summary>
    [JsonPropertyName("orgIDCardType")]
    public string? OrgIDCardType { get; set; }
    /// <summary>
    /// 法定代表人姓名
    /// </summary>
    [JsonPropertyName("legalRepName")]
    public string? LegalRepName { get; set; }
    /// <summary>
    /// 法定代表人证件号
    /// </summary>
    [JsonPropertyName("legalRepIDCardNum")]
    public string? LegalRepIDCardNum { get; set; }
    /// <summary>
    /// 法定代表人证件类型
    /// CRED_PSN_CH_IDCARD - 中国大陆居民身份证
    /// CRED_PSN_CH_HONGKONG - 香港来往大陆通行证（回乡证）
    /// CRED_PSN_CH_MACAO - 澳门来往大陆通行证（回乡证）
    /// CRED_PSN_CH_TWCARD - 台湾来往大陆通行证（台胞证）
    /// CRED_PSN_PASSPORT - 护照
    /// </summary>
    [JsonPropertyName("legalRepIDCardType")]
    public string? LegalRepIDCardType { get; set; }
    /// <summary>
    /// 机构管理员姓名
    /// </summary>
    [JsonPropertyName("adminName")]
    public string? AdminName { get; set; }
    /// <summary>
    /// 机构管理员联系方式
    /// </summary>
    [JsonPropertyName("adminAccount")]
    public string? AdminAccount { get; set; }

}
