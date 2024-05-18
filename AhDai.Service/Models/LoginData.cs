using System.Linq;
using System.Text.Json.Serialization;

namespace AhDai.Service.Models;

/// <summary>
/// 登录数据
/// </summary>
public class LoginData
{
	/// <summary>
	/// 用户Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 用户名
	/// </summary>
	public string Username { get; set; } = "";
	/// <summary>
	/// 姓名
	/// </summary>
	public string Name { get; set; } = "";
	/// <summary>
	/// 角色编码
	/// </summary>
	public string[] RoleCodes { get; set; } = [];
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
	/// 平台
	/// </summary>
	public string Platform { get; set; } = "";
	/// <summary>
	/// 员工Id
	/// </summary>
	public long EmployeeId { get; set; }
	/// <summary>
	/// 组织Id
	/// </summary>
	public long OrgId { get; set; }
	/// <summary>
	/// 租户Id
	/// </summary>
	public long TenantId { get; set; }
	/// <summary>
	/// 租户名称
	/// </summary>
	public string TenantName { get; set; } = "";
	/// <summary>
	/// 租户类型
	/// </summary>
	public Shared.Enums.TenantType TenantType { get; set; }
}
