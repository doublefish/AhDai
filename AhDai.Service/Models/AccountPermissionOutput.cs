using AhDai.Service.Sys.Models;

namespace AhDai.Service.Models;

/// <summary>
/// 账号权限
/// </summary>
public class AccountPermissionOutput
{
	/// <summary>
	/// 菜单权限：树形结构
	/// </summary>
	public MenuOutput[]? Menus { get; set; }
	/// <summary>
	/// 权限标识
	/// </summary>
	public string[]? Permissions { get; set; }

}
