namespace AhDai.Entity.Sys;

/// <summary>
/// 角色菜单
/// </summary>
public class RoleMenu : IKeyEntity
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 角色Id
	/// </summary>
	public long RoleId { get; set; }
	/// <summary>
	/// 菜单Id
	/// </summary>
	public long MenuId { get; set; }

	/// <summary>
	/// 构造函数
	/// </summary>
	public RoleMenu() { }

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="roleId"></param>
	/// <param name="menuId"></param>
	public RoleMenu(long roleId, long menuId)
	{
		RoleId = roleId;
		MenuId = menuId;
	}
}
