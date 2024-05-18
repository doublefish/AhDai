namespace AhDai.Service.Sys.Models;

/// <summary>
/// 用户数据权限
/// </summary>
public class UserDataPermissionOutput
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 组织Id
	/// </summary>
	public long OrgId { get; set; }
	/// <summary>
	/// 数据权限
	/// </summary>
	public Shared.Enums.DataPermission DataPermission { get; set; }
	/// <summary>
	/// 唯一编码
	/// </summary>
	public string UniqueCode { get; set; } = "";
	/// <summary>
	/// 层级
	/// </summary>
	public int Level { get; set; }
	/// <summary>
	/// 层级开始
	/// </summary>
	public int LevelFrom { get; set; }
	/// <summary>
	/// 层级结束
	/// </summary>
	public int LevelTo { get; set; }
}
