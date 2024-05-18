using System.Text.Json.Serialization;

namespace AhDai.Service;

/// <summary>
/// BaseQueryInput
/// </summary>
public abstract class BaseQueryInput : IBaseQueryInput
{
	/// <summary>
	/// Id
	/// </summary>
	public virtual long[]? Ids { get; set; }
	/// <summary>
	/// 排除的Id
	/// </summary>
	public virtual long[]? ExcludedIds { get; set; }
	/// <summary>
	/// 创建者Id
	/// </summary>
	public virtual long? CreatorId { get; set; }
	/// <summary>
	/// 创建者用户名
	/// </summary>
	public virtual string? CreatorUsername { get; set; }
	/// <summary>
	/// 创建者姓名
	/// </summary>
	public virtual string? CreatorName { get; set; }

	/// <summary>
	/// 页码（从1开始，分页接口有效）
	/// </summary>
	public virtual int? PageNo { get; set; }//PageNumber
	/// <summary>
	/// 每页条数
	/// </summary>
	public virtual int? PageSize { get; set; }
	/// <summary>
	/// 排序字段
	/// </summary>
	public virtual string? SortName { get; set; }
	/// <summary>
	/// 排序方式：asc/desc
	/// </summary>
	public virtual string? SortType { get; set; }

	/// <summary>
	/// 包括已删除的
	/// </summary>
	public virtual bool IncludeDeleted { get; set; }
	/// <summary>
	/// 数据深度
	/// </summary>
	public virtual int? DataDepth { get; set; }
	/// <summary>
	/// 数据权限
	/// </summary>
	[JsonIgnore]
	public virtual bool? WithDataPermission { get; set; }
}

