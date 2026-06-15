using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// TransIdentityInput
/// </summary>
public class TransIdentityInput
{
    /// <summary>
    /// 用户类型，PERSON或者ENTERPRISE
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("user_type")]
    public string UserType { get; set; } = default!;
    /// <summary>
    /// 证件类型
    /// 个人：IDENTITY_CARD（身份证）
    /// 企业：UNIFIED_SOCIAL_CREDIT_CODE（统一社会信用代码）、ENTERPRISE_REGISTERED_NUMBER（企业工商注册号）
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("cert_type")]
    public string CertType { get; set; } = default!;
    /// <summary>
    /// 存证主体名称
    /// 个人：身份证姓名
    /// 企业：企业证件上对应的企业名称
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("cert_name")]
    public string CertName { get; set; } = default!;
    /// <summary>
    /// 证件号，个人类型为身份证号，企业类型为统一社会信用代码或企业工商注册号
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("cert_no")]
    public string CertNo { get; set; } = default!;
    /// <summary>
    /// 用户手机号码
    /// </summary>
    [JsonPropertyName("mobile_no")]
    public string? MobileNo { get; set; }
    /// <summary>
    /// 法人姓名，企业类型必填
    /// </summary>
    [JsonPropertyName("legal_person")]
    public string? LegalPerson { get; set; }
    /// <summary>
    /// 法人证件号，企业类型必填
    /// </summary>
    [JsonPropertyName("legal_person_id")]
    public string? LegalPersonId { get; set; }
    /// <summary>
    /// 法人证件类型，企业类型必填，支持类型同certType的个人类型
    /// </summary>
    [JsonPropertyName("legal_person_cert_type")]
    public string? LegalPersonCertType { get; set; }
    /// <summary>
    /// 经办人姓名，企业类型选填
    /// </summary>
    [JsonPropertyName("agent")]
    public string? Agent { get; set; }
    /// <summary>
    /// 经办人证件号，企业类型选填
    /// </summary>
    [JsonPropertyName("agent_id")]
    public string? AgentId { get; set; }
    /// <summary>
    /// 经办人证件类型，企业类型选填，支持类型同certType的个人类型
    /// </summary>
    [JsonPropertyName("agent_cert_type")]
    public string? AgentCertType { get; set; }

}
