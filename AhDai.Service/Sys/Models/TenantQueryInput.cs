namespace AhDai.Service.Sys.Models;

/// <summary>
/// 租户查询入参
/// </summary>
public class TenantQueryInput : BaseQueryInput
{
	/// <summary>
	/// 名称：全模糊
	/// </summary>
	public string? Name { get; set; }
	/// <summary>
	/// 类型
	/// </summary>
	public Shared.Enums.TenantType? Type { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus? Status { get; set; }
}
