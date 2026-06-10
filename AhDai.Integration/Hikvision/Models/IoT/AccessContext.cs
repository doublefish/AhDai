namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// AccessContext
/// </summary>
public class AccessContext
{
    /// <summary>
    /// 应用访问凭证
    /// </summary>
    public string AppAccessToken { get; init; } = default!;
    /// <summary>
    /// 用户访问凭证
    /// </summary>
    public string UserAccessToken { get; init; } = default!;
    /// <summary>
    /// 刷新用户访问凭证
    /// </summary>
    public string? RefreshUserToken { get; init; }
}
