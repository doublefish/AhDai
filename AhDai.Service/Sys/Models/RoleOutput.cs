namespace AhDai.Service.Sys.Models;

/// <summary>
/// 角色
/// </summary>
public class RoleOutput : BaseOutput
{
	/// <summary>
	/// 编码
	/// </summary>
	public string Code { get; set; } = "";
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; } = "";
	/// <summary>
	/// 类型：0-普通，1-内置
	/// </summary>
	public int Type { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string? Remark { get; set; }
	/// <summary>
	/// 顺序
	/// </summary>
	public int Sort { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus Status { get; set; }
}
