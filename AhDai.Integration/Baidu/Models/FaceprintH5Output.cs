using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// FaceprintH5Output
/// </summary>
public class FaceprintH5Output<T> : IBaseOutput
{
    /// <summary>
    /// 请求是否成功
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    /// <summary>
    /// 请求结果
    /// </summary>
    [JsonPropertyName("result")]
    public T Result { get; set; } = default!;
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("code")]
    public int? Code { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    /// <summary>
    /// 调用的日志id
    /// </summary>
    [JsonPropertyName("log_id")]
    public string LogId { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (!Success || Result == null) throw new Exception($"请求百度云发生异常：[{Code}]{Message}，请联系管理员");
    }
}
