namespace AhDai.Service;

/// <summary>
/// IBaseQueryInput
/// </summary>
public interface IBaseQueryInput
{
	/// <summary>
	/// Ids
	/// </summary>
	public long[]? Ids { get; set; }
	/// <summary>
	/// 排除的Id
	/// </summary>
	public long[]? ExcludedIds { get; set; }
	/// <summary>
	/// 创建者Id
	/// </summary>
	public long? CreatorId { get; set; }
	/// <summary>
	/// 创建者用户名
	/// </summary>
	public string? CreatorUsername { get; set; }
	/// <summary>
	/// 创建者姓名
	/// </summary>
	public string? CreatorName { get; set; }

	/// <summary>
	/// 页码（从1开始，分页接口有效）
	/// </summary>
	public int? PageNo { get; set; }//PageNumber
	/// <summary>
	/// 每页条数
	/// </summary>
	public int? PageSize { get; set; }
	/// <summary>
	/// 排序字段
	/// </summary>
	public string? SortName { get; set; }
	/// <summary>
	/// 排序方式：asc/desc
	/// </summary>
	public string? SortType { get; set; }

	/// <summary>
	/// 包括已删除的
	/// </summary>
	public bool IncludeDeleted { get; set; }
	/// <summary>
	/// 数据深度
	/// </summary>
	public int? DataDepth { get; set; }
	/// <summary>
	/// 数据权限
	/// </summary>
	public bool? WithDataPermission { get; set; }

}

