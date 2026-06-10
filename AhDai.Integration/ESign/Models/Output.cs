using AhDai.Integration.Abstractions;
using System;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// BaseOutput
/// </summary>
/// <typeparam name="T"></typeparam>
public class Output<T> : IBaseOutput
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

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Code != 0) throw new Exception($"请求电子签发生异常：[{Code}]{Message}，请联系管理员");
    }
}
