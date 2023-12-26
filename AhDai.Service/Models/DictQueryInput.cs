namespace AhDai.Service.Models;

/// <summary>
/// 字典查询入参
/// </summary>
public class DictQueryInput : BaseQueryInput
{
	/// <summary>
	/// 编码
	/// </summary>
	public string Code { get; set; }
	/// <summary>
	/// 编码
	/// </summary>
	public string[] Codes { get; set; }
	/// <summary>
	/// 名称：全模糊
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public int? Status { get; set; }
}
