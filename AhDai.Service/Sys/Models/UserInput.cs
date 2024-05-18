using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 用户
/// </summary>
public class UserInput : BaseInput
{
	/// <summary>
	/// 用户名：不可修改
	/// </summary>
	[Required]
	public string Username { get; set; } = "";
	/// <summary>
	/// 头像Id
	/// </summary>
	[JsonIgnore]
	public long? AvatarId { get; set; }
	/// <summary>
	/// 昵称
	/// </summary>
	[JsonIgnore]
	public string? Nickname { get; set; }
	/// <summary>
	/// 姓名
	/// </summary>
	[Required]
	public string Name { get; set; } = "";
	/// <summary>
	/// 生日
	/// </summary>
	public DateTime? Birthday { get; set; }
	/// <summary>
	/// 性别
	/// </summary>
	[Required]
	public Shared.Enums.Gender Gender { get; set; }
	/// <summary>
	/// 邮箱
	/// </summary>
	public string? Email { get; set; }
	/// <summary>
	/// 手机：不可修改
	/// </summary>
	[Required]
	public string MobilePhone { get; set; } = "";
	/// <summary>
	/// 电话
	/// </summary>
	public string? Telephone { get; set; }
	/// <summary>
	/// 所属组织
	/// </summary>
	public UserOrgInput[]? Orgs { get; set; }
	/// <summary>
	/// 角色Id
	/// </summary>
	public long[]? RoleIds { get; set; }
}
