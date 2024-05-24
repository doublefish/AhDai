namespace AhDai.Core;

/// <summary>
/// IApiResult
/// </summary>
public interface IApiResult
{
	/// <summary>
	/// 状态代码
	/// </summary>
	public int Code { get; set; }
	/// <summary>
	/// 消息
	/// </summary>
	public string Message { get; set; }
	/// <summary>
	/// 追踪Id
	/// </summary>
	public string TraceId { get; set; }

}

/// <summary>
/// IApiResult
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IApiResult<T> : IApiResult
{
	/// <summary>
	/// 结果
	/// </summary>
	public T Data { get; set; }
	/// <summary>
	/// 扩展数据
	/// </summary>
	public object? ExtraData { get; set; }
}
