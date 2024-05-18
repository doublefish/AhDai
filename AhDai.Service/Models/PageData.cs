namespace AhDai.Service.Models;

/// <summary>
/// PageData
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="totalRows"></param>
/// <param name="rows"></param>
public class PageData<T>(int totalRows, T[] rows = default!)
{
	/// <summary>
	/// 总行数
	/// </summary>
	public int TotalRows { get; set; } = totalRows;
	/// <summary>
	/// 数据
	/// </summary>
	public T[] Rows { get; set; } = rows;
}
