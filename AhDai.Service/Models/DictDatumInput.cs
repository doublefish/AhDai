using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Models;

/// <summary>
/// 字典数据
/// </summary>
public class DictDatumInput
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 字典Id
	/// </summary>
	[Required]
	public long DictId { get; set; }
	/// <summary>
	/// 编码
	/// </summary>
	public string Code { get; set; }
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 值
	/// </summary>
	public string Value { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string Remark { get; set; }
	/// <summary>
	/// 顺序
	/// </summary>
	public int Sort { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public int Status { get; set; }
}
