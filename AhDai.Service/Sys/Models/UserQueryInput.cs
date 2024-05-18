namespace AhDai.Service.Sys.Models;

/// <summary>
/// 用户查询入参
/// </summary>
public class UserQueryInput : BaseQueryInput
{
	/// <summary>
	/// 用户名
	/// </summary>
	public string? Username { get; set; }
	/// <summary>
	/// 用户名
	/// </summary>
	public string[]? Usernames { get; set; }
	/// <summary>
	/// 名称：全模糊
	/// </summary>
	public string? Name { get; set; }
	/// <summary>
	/// 组织Id
	/// </summary>
	public long? OrgId { get; set; }
	/// <summary>
	/// 组织Id
	/// </summary>
	public long[]? OrgIds { get; set; }
	/// <summary>
	/// 角色Id
	/// </summary>
	public long? RoleId { get; set; }
	/// <summary>
	/// 角色Id
	/// </summary>
	public long[]? RoleIds { get; set; }
	/// <summary>
	/// 角色编码
	/// </summary>
	public string? RoleCode { get; set; }
	/// <summary>
	/// 角色编码
	/// </summary>
	public string[]? RoleCodes { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus? Status { get; set; }
}
