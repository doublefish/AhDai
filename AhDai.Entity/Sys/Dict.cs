namespace AhDai.Entity.Sys;

/// <summary>
/// 字典
/// </summary>
public class Dict : BaseTenantEntity, ICodeEntity, IParentIdEntity
{
	/// <summary>
	/// 编码
	/// </summary>
	public string Code { get; set; } = "";
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; } = "";
	/// <summary>
	/// 父级Id
	/// </summary>
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
	/// 顺序
	/// </summary>
	public int Sort { get; set; }
	/// <summary>
	/// 状态：1-正常，2-停用
	/// </summary>
	public Shared.Enums.GenericStatus Status { get; set; }

}
