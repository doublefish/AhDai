namespace AhDai.Service.Models;

/// <summary>
/// 统计数据
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class StatisticData<TKey>
{
	/// <summary>
	/// 键/维度
	/// </summary>
	public TKey Key { get; set; } = default!;
    /// <summary>
    /// 键名/维度名称
    /// </summary>
    public string KeyName { get; set; } = "";
	/// <summary>
	/// 总和
	/// </summary>
	public decimal TotalAmount { get; set; }
	/// <summary>
	/// 总数
	/// </summary>
	public int TotalCount { get; set; }

}
