namespace AhDai.Service.Sys.Models;

/// <summary>
/// 员工查询入参
/// </summary>
public class EmployeeQueryInput : BaseQueryInput, INumberQueryInput
{
	/// <summary>
	/// 工号
	/// </summary>
	public string? Number { get; set; }
	/// <summary>
	/// 工号
	/// </summary>
	public string[]? Numbers { get; set; }
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
	/// 岗位Id
	/// </summary>
	public long? PostId { get; set; }
	/// <summary>
	/// 岗位Id
	/// </summary>
	public long[]? PostIds { get; set; }
	/// <summary>
	/// 用户Id
	/// </summary>
	public long? UserId { get; set; }
	/// <summary>
	/// 用户Id
	/// </summary>
	public long[]? UserIds { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus? Status { get; set; }
}
