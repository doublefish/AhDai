using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 机构
/// </summary>
public class OrgOutput
{
    /// <summary>
    /// 实名认证状态：0 - 未实名，1 - 已实名
    /// </summary>
    [JsonPropertyName("realnameStatus")]
    public int RealnameStatus { get; set; }
    /// <summary>
    /// 是否授权身份信息给当前应用
    /// </summary>
    [JsonPropertyName("authorizeUserInfo")]
    public bool AuthorizeUserInfo { get; set; }
    /// <summary>
    /// 机构账号ID
    /// </summary>
    [JsonPropertyName("orgId")]
    public string OrgId { get; set; } = default!;
    /// <summary>
    /// 机构名称
    /// </summary>
    [JsonPropertyName("orgName")]
    public string OrgName { get; set; } = default!;
    /// <summary>
    /// 机构认证信息
    /// </summary>
    [JsonPropertyName("orgInfo")]
    public OrgAuthOutput? OrgInfo { get; set; }
}
