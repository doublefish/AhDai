namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// AccessContext
/// </summary>
/// <param name="appAccessToken">应用访问凭证</param>
/// <param name="userAccessToken">用户访问凭证</param>
/// <param name="refreshUserToken">刷新用户访问凭证</param>
public class AccessContext(string appAccessToken, string userAccessToken, string? refreshUserToken = null)
{
    /// <summary>
    /// 应用访问凭证
    /// </summary>
    public string AppAccessToken { get; init; } = appAccessToken;
    /// <summary>
    /// 用户访问凭证
    /// </summary>
    public string UserAccessToken { get; init; } = userAccessToken;
    /// <summary>
    /// 刷新用户访问凭证
    /// </summary>
    public string? RefreshUserToken { get; init; } = refreshUserToken;
}
