namespace AhDai.Service.Sys.Models;

/// <summary>
/// 接口查询入参
/// </summary>
public class InterfaceQueryInput : BaseQueryInput
{
	/// <summary>
	/// 名称
	/// </summary>
	public string? Name { get; set; }
	/// <summary>
	/// 方法
	/// </summary>
	public string? Method { get; set; }
	/// <summary>
	/// Url
	/// </summary>
	public string? Url { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }
}
