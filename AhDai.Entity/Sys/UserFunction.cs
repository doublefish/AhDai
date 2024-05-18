namespace AhDai.Entity.Sys;

/// <summary>
/// 用户功能
/// </summary>
public class UserFunction : BaseTenantEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// 菜单Id
    /// </summary>
    public long MenuId { get; set; }
    /// <summary>
    /// 功能Id
    /// </summary>
    public long FunctionId { get; set; }
}
