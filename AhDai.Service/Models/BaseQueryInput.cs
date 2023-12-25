namespace AhDai.Service.Models;

/// <summary>
/// BaseQueryInput
/// </summary>
public class BaseQueryInput
{
	/// <summary>
	/// 页码（从1开始，分页接口有效）
	/// </summary>
	public int? PageNumber { get; set; }
	/// <summary>
	/// 每页条数
	/// </summary>
	public int? PageSize { get; set; }
	/// <summary>
	/// 排序字段
	/// </summary>
	public string SortName { get; set; }
	/// <summary>
	/// 排序方式：asc/desc
	/// </summary>
	public string SortType { get; set; }
	
}
