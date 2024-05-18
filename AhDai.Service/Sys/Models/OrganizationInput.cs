using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 组织
/// </summary>
public class OrganizationInput : BaseInput
{
	/// <summary>
	/// 编码
	/// </summary>
	[Required]
	public string Code { get; set; } = "";
	/// <summary>
	/// 名称
	/// </summary>
	[Required]
	public string Name { get; set; } = "";
	/// <summary>
	/// 父级Id
	/// </summary>
	public long ParentId { get; set; }
	/// <summary>
	/// 负责人Id
	/// </summary>
	public long? LeaderId { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string? Remark { get; set; }
	/// <summary>
	/// 顺序：0-999
	/// </summary>
	[Range(0, 999)]
	public int Sort { get; set; } = 999;
}
