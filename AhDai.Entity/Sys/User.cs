using System;

namespace AhDai.Entity.Sys;

/// <summary>
/// 用户
/// </summary>
public class User : BaseTenantEntity
{
	/// <summary>
	/// 用户名
	/// </summary>
	public string Username { get; set; } = "";
	/// <summary>
	/// 头像
	/// </summary>
	public long? AvatarId { get; set; }
	/// <summary>
	/// 昵称
	/// </summary>
	public string? Nickname { get; set; }
	/// <summary>
	/// 姓名
	/// </summary>
	public string Name { get; set; } = "";
	/// <summary>
	/// 生日
	/// </summary>
	public DateTime? Birthday { get; set; }
	/// <summary>
	/// 性别
	/// </summary>
	public Shared.Enums.Gender Gender { get; set; }
	/// <summary>
	/// 邮箱
	/// </summary>
	public string? Email { get; set; }
	/// <summary>
	/// 手机
	/// </summary>
	public string MobilePhone { get; set; } = "";
	/// <summary>
	/// 电话
	/// </summary>
	public string? Telephone { get; set; }
	/// <summary>
	/// 状态：1-正常，2-停用
	/// </summary>
	public Shared.Enums.GenericStatus Status { get; set; }
}
