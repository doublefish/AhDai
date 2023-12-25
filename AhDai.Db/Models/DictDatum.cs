namespace AhDai.Db.Models;

/// <summary>
/// 字典项
/// </summary>
public partial class DictDatum : BaseModel
{
	/// <summary>
	/// 字典Id
	/// </summary>
	public int DictId { get; set; }
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
