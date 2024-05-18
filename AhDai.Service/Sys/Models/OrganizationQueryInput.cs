namespace AhDai.Service.Sys.Models;

/// <summary>
/// 组织查询入参
/// </summary>
public class OrganizationQueryInput : BaseQueryInput
{
	/// <summary>
	/// 关键字
	/// </summary>
	public string? Keyword { get; set; }
	/// <summary>
	/// 编码
	/// </summary>
	public string? Code { get; set; }
	/// <summary>
	/// 编码
	/// </summary>
	public string[]? Codes { get; set; }
	/// <summary>
	/// 名称：全模糊
	/// </summary>
	public string? Name { get; set; }
	/// <summary>
	/// 组织Id
	/// </summary>
	public long? ParentId { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public Shared.Enums.GenericStatus? Status { get; set; }
}
