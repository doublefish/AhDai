using System.ComponentModel.DataAnnotations.Schema;

namespace AhDai.Db.Models;

/// <summary>
/// 接口
/// </summary>
public partial class Interface : BaseModel
{
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 方法
	/// </summary>
	public string Method { get; set; }
	/// <summary>
	/// Url
	/// </summary>
	public string Url { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string Remark { get; set; }
	/// <summary>
	/// 状态
	/// </summary>
	public int Status { get; set; }
}
