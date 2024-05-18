using System.Collections.Generic;

namespace AhDai.Service.Base;

/// <summary>
/// MessageData
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="headers"></param>
/// <param name="body"></param>
public class MessageData<T>(IDictionary<string, string>? headers, T? body)
{
	/// <summary>
	/// 头
	/// </summary>
	public IDictionary<string, string> Headers { get; set; } = headers ?? new Dictionary<string, string>();
	/// <summary>
	/// 数据
	/// </summary>
	public T Body { get; set; } = body ?? default!;

	/// <summary>
	/// 构造函数
	/// </summary>
	public MessageData() : this(null, default)
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="body"></param>
	public MessageData(T body) : this(null, body)
	{
	}
}
