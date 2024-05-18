using AhDai.Service.Models;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 用户配置
/// </summary>
public class UserConfigOutput
{
	/// <summary>
	/// 数据权限类型
	/// </summary>
	public ValueNameData<Shared.Enums.DataPermission>[] DataPermissions { get; set; } = default!;
}
