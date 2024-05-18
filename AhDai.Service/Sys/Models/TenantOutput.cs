namespace AhDai.Service.Sys.Models;

/// <summary>
/// 租户
/// </summary>
public class TenantOutput : BaseOutput
{
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; } = "";
	/// <summary>
	/// 类型
	/// </summary>
	public Shared.Enums.TenantType Type { get; set; }
	/// <summary>
	/// 管理员Id
	/// </summary>
	public long AdminId { get; set; }
	/// <summary>
	/// 管理员
	/// </summary>
	public UserOutput? Admin { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string? Remark { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus Status { get; set; }
}
