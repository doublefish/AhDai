namespace AhDai.Entity.Sys;

/// <summary>
/// 用户组织
/// </summary>
public class UserOrg : BaseTenantEntity
{
	/// <summary>
	/// 用户Id
	/// </summary>
	public long UserId { get; set; }
	/// <summary>
	/// 组织Id
	/// </summary>
	public long OrgId { get; set; }
	/// <summary>
	/// 数据权限
	/// </summary>
	public Shared.Enums.DataPermission DataPermission { get; set; }
	/// <summary>
	/// 是否默认
	/// </summary>
	public bool IsDefault { get; set; }
}
