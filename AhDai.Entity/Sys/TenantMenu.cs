namespace AhDai.Entity.Sys;

/// <summary>
/// 租户菜单
/// </summary>
public class TenantMenu : IKeyEntity
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 角色Id
	/// </summary>
	public long TenantId { get; set; }
	/// <summary>
	/// 菜单Id
	/// </summary>
	public long MenuId { get; set; }

	/// <summary>
	/// 构造函数
	/// </summary>
	public TenantMenu() { }

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="tenantId"></param>
	/// <param name="menuId"></param>
	public TenantMenu(long tenantId, long menuId)
	{
		TenantId = tenantId;
		MenuId = menuId;
	}
}
