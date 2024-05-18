namespace AhDai.Entity.Sys;

/// <summary>
/// 角色功能
/// </summary>
public class RoleFunction : BaseTenantEntity
{
    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public int FunctionId { get; set; }
}
