using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 个人
/// </summary>
public class PersonOutput
{
    /// <summary>
    /// 实名认证状态：0 - 未实名，1 - 已实名
    /// </summary>
    [JsonPropertyName("realnameStatus")]
    public int RealnameStatus { get; set; }
    /// <summary>
    /// 是否授权相关信息给当前应用
    /// </summary>
    [JsonPropertyName("authorizeUserInfo")]
    public bool AuthorizeUserInfo { get; set; }
    /// <summary>
    /// 个人账号ID
    /// </summary>
    [JsonPropertyName("psnId")]
    public string PsnId { get; set; } = default!;
    /// <summary>
    /// 个人账号标识
    /// </summary>
    [JsonPropertyName("psnAccount")]
    public PersonAccountOutput? PsnAccount { get; set; }
    /// <summary>
    /// 个人用户身份信息
    /// </summary>
    [JsonPropertyName("psnInfo")]
    public PersonAuthOutput? PsnInfo { get; set; }
}
