using System;

namespace AhDai.Entity;

/// <summary>
/// IBaseEntity
/// </summary>
public interface IBaseEntity : IKeyEntity
{
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
	/// 删除标识
	/// </summary>
	public bool IsDeleted { get; set; }
	/// <summary>
	/// 删除时间
	/// </summary>
	public DateTime? DeletionTime { get; set; }
	/// <summary>
	/// 删除者Id
	/// </summary>
	public long? DeleterId { get; set; }
	/// <summary>
	/// 删除者用户名
	/// </summary>
	public string? DeleterUsername { get; set; }
}
