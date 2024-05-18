namespace AhDai.Entity.Sys;

/// <summary>
/// 用户密码
/// </summary>
public class UserPassword : BaseTenantEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = "";
}
