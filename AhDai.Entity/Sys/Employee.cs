using System;

namespace AhDai.Entity.Sys;

/// <summary>
/// Employee
/// </summary>
public class Employee : BaseTenantEntity, INumberEntity
{
	/// <summary>
	/// 工号
	/// </summary>
	public string Number { get; set; } = "";
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
    /// 身份证号
    /// </summary>
    public string IdNumber { get; set; } = "";
    /// <summary>
    /// 组织Id
    /// </summary>
    public long OrgId { get; set; }
	/// <summary>
	/// 岗位Id
	/// </summary>
	public string? PostIds { get; set; }
	/// <summary>
	/// 用户Id
	/// </summary>
	public long UserId { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus Status { get; set; }

}
