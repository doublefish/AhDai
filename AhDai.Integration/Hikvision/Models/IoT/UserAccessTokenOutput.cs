using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 用户访问凭证
/// </summary>
public class UserAccessTokenOutput
{
    /// <summary>
    /// AppKey
    /// </summary>
    [JsonPropertyName("appKey")]
    public string AppKey { get; set; } = default!;
    /// <summary>
    /// 用户访问凭证
    /// </summary>
    [JsonPropertyName("userAccessToken")]
    public string UserAccessToken { get; set; } = default!;
    /// <summary>
    /// 刷新用户访问凭证
    /// </summary>
    [JsonPropertyName("refreshUserToken")]
    public string RefreshUserToken { get; set; } = default!;
    /// <summary>
    /// 有效时间，单位：天
    /// </summary>
    [JsonPropertyName("expiresIn")]
    public string ExpiresIn { get; set; } = default!;
    /// <summary>
    /// 团队编号
    /// </summary>
    [JsonPropertyName("teamNo")]
    public string TeamNo { get; set; } = default!;
    /// <summary>
    /// 成员编号
    /// </summary>
    [JsonPropertyName("personNo")]
    public string PersonNo { get; set; } = default!;
    /// <summary>
    /// 海康通行证账号
    /// </summary>
    [JsonPropertyName("accountNo")]
    public string AccountNo { get; set; } = default!;
}
