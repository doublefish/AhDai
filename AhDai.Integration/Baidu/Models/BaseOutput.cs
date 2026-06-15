using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// BaseOutput
/// </summary>
public abstract class BaseOutput : IBaseOutput
{
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("error")]
    public virtual string? Error { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonPropertyName("error_description")]
    public virtual string? ErrorMessage { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (!string.IsNullOrEmpty(Error)) throw new Exception($"请求百度云发生异常：[{Error}]{ErrorMessage}，请联系管理员");
    }
}
