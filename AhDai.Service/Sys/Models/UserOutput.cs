using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 用户
/// </summary>
public class UserOutput : BaseOutput
{
	/// <summary>
	/// 用户名
	/// </summary>
	public string Username { get; set; } = "";
	/// <summary>
	/// 头像Id
	/// </summary>
	[JsonIgnore]
	public long? AvatarId { get; set; }
	/// <summary>
	/// 头像地址
	/// </summary>
	public string? AvatarUrl { get; set; }
	/// <summary>
	/// 昵称
	/// </summary>
	public string? Nickname { get; set; }
	/// <summary>
	/// 名称
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
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus Status { get; set; }
	/// <summary>
	/// 组织
	/// </summary>
	public UserOrgOutput[]? Orgs { get; set; }
	/// <summary>
	/// 默认组织
	/// </summary>
	[JsonIgnore]
	public UserOrgOutput? DefaultOrg => Orgs?.Where(x => x.IsDefault == true).FirstOrDefault();
	/// <summary>
	/// 默认组织Id
	/// </summary>
	public long? OrgId => DefaultOrg?.OrgId;
	/// <summary>
	/// 默认组织名称
	/// </summary>
	public string? OrgName => DefaultOrg?.OrgName;
	/// <summary>
	/// 角色Id
	/// </summary>
	public long[]? RoleIds { get; set; }
	/// <summary>
	/// 角色编码
	/// </summary>
	public string[]? RoleCodes => Roles?.Select(x => x.Code).ToArray();
	/// <summary>
	/// 是否管理员
	/// </summary>
	[JsonIgnore]
	public bool IsAdmin => RoleCodes != null && RoleCodes.Contains(Shared.Consts.RoleCode.Admin);
	/// <summary>
	/// 是否人事
	/// </summary>
	[JsonIgnore]
	public bool IsHr => RoleCodes != null && RoleCodes.Contains(Shared.Consts.RoleCode.HR);
	/// <summary>
	/// 角色
	/// </summary>
	public RoleOutput[]? Roles { get; set; }

	/// <summary>
	/// 微信OpenId
	/// </summary>
	[JsonIgnore]
	public virtual string? WeChatOpenId { get; set; }
	/// <summary>
	/// ErpId
	/// </summary>
	[JsonIgnore]
	public virtual string? ErpUserId { get; set; }
	/// <summary>
	/// 乐筑一站Id
	/// </summary>
	[JsonIgnore]
	public virtual string? LzezUserId { get; set; }

	/// <summary>
	/// 租戶名称
	/// </summary>
	public string TenantName { get; set; } = "";
	/// <summary>
	/// 租戶类型
	/// </summary>
	public Shared.Enums.TenantType TenantType { get; set; }
}
