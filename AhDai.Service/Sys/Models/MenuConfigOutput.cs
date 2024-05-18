using AhDai.Service.Models;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 菜单配置
/// </summary>
public class MenuConfigOutput
{
	/// <summary>
	/// 数据权限类型
	/// </summary>
	public ValueNameData<Shared.Enums.MenuType>[] Types { get; set; } = default!;
}
