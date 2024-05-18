using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 接口
/// </summary>
public class InterfaceInput : BaseInput
{
	/// <summary>
	/// 名称
	/// </summary>
	[Required]
	public string Name { get; set; } = "";
	/// <summary>
	/// 方法
	/// </summary>
	[Required]
	public string Method { get; set; } = "";
	/// <summary>
	/// Url
	/// </summary>
	[Required]
	public string Url { get; set; } = "";
	/// <summary>
	/// 备注
	/// </summary>
	public string? Remark { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public int Status { get; set; }
}
