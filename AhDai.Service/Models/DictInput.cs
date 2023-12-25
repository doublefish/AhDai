namespace AhDai.Service.Models;

/// <summary>
/// 字典
/// </summary>
public class DictInput
{
	/// <summary>
	/// 编码
	/// </summary>
	public string Code { get; set; }
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string Remark { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public int Status { get; set; }
	/// <summary>
	/// 数据
	/// </summary>
	public DictDatumInput[] Data { get; set; }
}
