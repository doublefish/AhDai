using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Tianyancha.Models;

/// <summary>
/// Output
/// </summary>
/// <typeparam name="T"></typeparam>
public class Output<T> : IBaseOutput
{
    /// <summary>
    /// 错误代码，返回 0 代表请求成功
    /// </summary>
    [JsonPropertyName("error_code")]
    public int ErrorCode { get; set; }
    /// <summary>
    /// 错误原因
    /// </summary>
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;
    /// <summary>
    /// 结果
    /// </summary>
    [JsonPropertyName("result")]
    public T? Result { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void EnsureResult()
    {
        if (ErrorCode != 0) throw new Exception($"请求天眼查发生异常：[{ErrorCode}]{Reason}");
    }
}
