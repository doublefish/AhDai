namespace AhDai.Service;

/// <summary>
/// 金额查询入参
/// </summary>
public interface IAmountQueryInput
{
    /// <summary>
	/// 金额
	/// </summary>
	public decimal? Amount { get; set; }
    /// <summary>
    /// 金额.最小
    /// </summary>
    public decimal? AmountMin { get; set; }
    /// <summary>
    /// 金额.最大
    /// </summary>
    public decimal? AmountMax { get; set; }
}
