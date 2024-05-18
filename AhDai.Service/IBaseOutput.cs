using System;
using System.Text.Json.Serialization;

namespace AhDai.Service;

/// <summary>
/// IBaseOutput
/// </summary>
public interface IBaseOutput
{
	/// <summary>
	/// 主键
	/// </summary>
	public long Id { get; set; }
	/// <summary>
	/// 版本
	/// </summary>
	public int Version { get; set; }
	/// <summary>
	/// 创建时间
	/// </summary>
	public DateTime CreationTime { get; set; }
	/// <summary>
	/// 创建者Id
	/// </summary>
	public long CreatorId { get; set; }
	/// <summary>
	/// 创建者用户名
	/// </summary>
	public string CreatorUsername { get; set; }
	/// <summary>
	/// 创建者姓名
	/// </summary>
	public string CreatorName { get; set; }
	/// <summary>
	/// 修改时间
	/// </summary>
	public DateTime? ModificationTime { get; set; }
	/// <summary>
	/// 修改者Id
	/// </summary>
	public long? ModifierId { get; set; }
	/// <summary>
	/// 修改者用户名
	/// </summary>
	public string? ModifierUsername { get; set; }
	/// <summary>
	/// 修改者姓名
	/// </summary>
	public string? ModifierName { get; set; }
	/// <summary>
	/// 删除标识
	/// </summary>
	public bool IsDeleted { get; set; }
	/// <summary>
	/// 删除时间
	/// </summary>
	[JsonIgnore]
	public DateTime? DeletionTime { get; set; }
	/// <summary>
	/// 删除者Id
	/// </summary>
	[JsonIgnore]
	public long? DeleterId { get; set; }
	/// <summary>
	/// 删除者用户名
	/// </summary>
	[JsonIgnore]
	public string? DeleterUsername { get; set; }
	/// <summary>
	/// 租戶Id
	/// </summary>
	public long TenantId { get; set; }
}

