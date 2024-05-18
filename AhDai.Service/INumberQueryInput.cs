namespace AhDai.Service;

/// <summary>
/// 编号查询入参
/// </summary>
public interface INumberQueryInput
{
	/// <summary>
	/// 编号
	/// </summary>
	public string? Number { get; set; }
	/// <summary>
	/// 编号
	/// </summary>
	public string[]? Numbers { get; set; }
}
