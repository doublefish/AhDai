using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 个人认证信息
/// </summary>
public class PersonAuthOutput
{
    /// <summary>
    /// 个人用户已认证的姓名
    /// </summary>
    [JsonPropertyName("psnName")]
    public string PsnName { get; set; } = default!;
    /// <summary>
    /// 个人国籍（暂无值返回）
    /// </summary>
    [JsonPropertyName("psnNationality")]
    public string? PsnNationality { get; set; }
    /// <summary>
    /// 个人证件号
    /// </summary>
    [JsonPropertyName("psnIDCardNum")]
    public string? PsnIDCardNum { get; set; }
    /// <summary>
    /// 证件类型
    /// CRED_PSN_CH_IDCARD - 中国大陆居民身份证
    /// CRED_PSN_CH_HONGKONG - 香港来往大陆通行证（回乡证）
    /// CRED_PSN_CH_MACAO - 澳门来往大陆通行证（回乡证）
    /// CRED_PSN_CH_TWCARD - 台湾来往大陆通行证（台胞证）
    /// CRED_PSN_PASSPORT - 护照
    /// </summary>
    [JsonPropertyName("psnIDCardType")]
    public string? PsnIDCardType { get; set; }
    /// <summary>
    /// 个人用户已认证的银行卡号
    /// </summary>
    [JsonPropertyName("bankCardNum")]
    public string? BankCardNum { get; set; }
    /// <summary>
    /// 个人用户已认证的运营商实名登记手机号或银行卡预留手机号
    /// </summary>
    [JsonPropertyName("psnMobile")]
    public string? PsnMobile { get; set; }

}
