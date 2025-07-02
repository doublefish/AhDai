namespace AhDai.Core;

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
    public string Message { get; set; } = null!;
    /// <summary>
    /// 追踪Id
    /// </summary>
    public string TraceId { get; set; } = null!;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ApiResult() { }

    /// <summary>
    /// 返回成功
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult<T> Success<T>(T data)
    {
        return new ApiResult<T>(data);
    }

    /// <summary>
    /// 返回成功
    /// </summary>
    /// <returns></returns>
    public static ApiResult<string> Success()
    {
        return new ApiResult<string>("");
    }

    /// <summary>
    /// 返回错误
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="extraData"></param>
    /// <returns></returns>
    public static ApiResult<string> Error(int code = 500, string message = "", object? extraData = null)
    {
        return new ApiResult<string>(code, message, "")
        {
            ExtraData = extraData
        };
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
    public object? ExtraData { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public ApiResult() : this(0, "", default!)
    {
    }

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
    public ApiResult(int code, string message, T data)
    {
        Code = code;
        Message = message;
        Data = data;
    }
}