namespace AhDai.Service.Models;

/// <summary>
/// 角色
/// </summary>
public class RoleInput
{
	/// <summary>
	/// 编码
	/// </summary>
	public string Code { get; set; }
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string Remark { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public int Status { get; set; }
}
