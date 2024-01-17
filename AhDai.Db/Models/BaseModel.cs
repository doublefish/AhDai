using System;

namespace AhDai.Db.Models;

/// <summary>
/// BaseModel
/// </summary>
public class BaseModel : BaseModel<long>
{
}

/// <summary>
/// BaseModel
/// </summary>
public class BaseModel<PK>
{
	/// <summary>
	/// 主键
	/// </summary>
	public PK Id { get; set; }
	/// <summary>
	/// 版本
	/// </summary>
	public int RowVersion { get; set; }
	/// <summary>
	/// 创建用户
	/// </summary>
	public int RowCreateUser { get; set; }
	/// <summary>
	/// 创建用户名
	/// </summary>
	public string RowCreateUsername { get; set; }
	/// <summary>
	/// 创建时间
	/// </summary>
	public DateTime RowCreateTime { get; set; }
	/// <summary>
	/// 更新用户
	/// </summary>
	public int RowUpdateUser { get; set; }
	/// <summary>
	/// 更新用户名
	/// </summary>
	public string RowUpdateUsername { get; set; }
	/// <summary>
	/// 更新时间
	/// </summary>
	public DateTime RowUpdateTime { get; set; }
	/// <summary>
	/// 删除标识
	/// </summary>
	public bool RowDeleted { get; set; }
}
