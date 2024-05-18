using AhDai.Service.Models;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 租户配置
/// </summary>
public class TenantConfigOutput
{
	/// <summary>
	/// 数据权限类型
	/// </summary>
	public ValueNameData<Shared.Enums.TenantType>[] Types { get; set; } = default!;
}
