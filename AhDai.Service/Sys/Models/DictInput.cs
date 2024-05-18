using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 字典
/// </summary>
public class DictInput : BaseInput
{
	/// <summary>
	/// 编码：一级节点必填，且不可修改
	/// </summary>
	//[Required]
	public string Code { get; set; } = "";
	/// <summary>
	/// 名称
	/// </summary>
	[Required]
	public string Name { get; set; } = "";
	/// <summary>
	/// 父级Id
	/// </summary>
	[Range(0, long.MaxValue)]
	[Required]
	public long ParentId { get; set; }
	/// <summary>
	/// 值
	/// </summary>
	public string Value { get; set; } = "";
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
