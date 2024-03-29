﻿namespace AhDai.Core;

/// <summary>
/// ApiResult
/// </summary>
public class ApiResult : IApiResult
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

	/// <summary>
	/// 构造函数
	/// </summary>
	public ApiResult() { }

	/// <summary>
	/// 返回成功
	/// </summary>
	/// <returns></returns>
	public static ApiResult<string> Success()
	{
		return new ApiResult<string>("");
	}

	/// <summary>
	/// 返回成功
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="data"></param>
	/// <returns></returns>
	public static ApiResult<T> Success<T>(T data = default)
	{
		return new ApiResult<T>(data);
	}

	/// <summary>
	/// 返回错误
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="code"></param>
	/// <param name="message"></param>
	/// <param name="data"></param>
	/// <returns></returns>
	public static ApiResult<T> Error<T>(int code = 500, string message = "", T data = default)
	{
		return new ApiResult<T>(code, message, data);
	}
}

/// <summary>
/// 接口结果
/// </summary>
public class ApiResult<T> : ApiResult, IApiResult<T>
{
	/// <summary>
	/// 结果
	/// </summary>
	public T Data { get; set; }
	/// <summary>
	/// 扩展数据
	/// </summary>
	public object ExtraData { get; set; }

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="data">内容</param>
	public ApiResult(T data) : this(200, "ok", data)
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="code">结果代码</param>
	/// <param name="message">消息</param>
	/// <param name="data">内容</param>
	public ApiResult(int code = 200, string message = "ok", T data = default)
	{
		Code = code;
		Message = message;
		Data = data;
	}
}