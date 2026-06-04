namespace AhDai.Integration.ESign.Models;

/// <summary>
/// BaseOutput
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseOutput<T>
{
    /// <summary>
    /// 业务码，0表示成功，非0表示异常
    /// </summary>
    public int Code { get; set; }
    /// <summary>
    /// 业务信息
    /// </summary>
    public string Message { get; set; } = default!;
    /// <summary>
    /// 业务数据
    /// </summary>
    public T? Data { get; set; }
}
