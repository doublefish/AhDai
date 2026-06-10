using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// Output
/// </summary>
/// <typeparam name="T"></typeparam>
public class Output<T> : IBaseOutput
{
    /// <summary>
    /// 状态码
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
    /// <summary>
    /// 结果描述信息
    /// </summary>
    [JsonPropertyName("msg")]
    public string? Msg { get; set; }
    /// <summary>
    /// 结果描述信息
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }
    /// <summary>
    /// 总数
    /// </summary>
    [JsonPropertyName("count")]
    public long Count { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Code != 0) throw new Exception($"请求海康互联发生异常：[{Code}]{Msg}，请联系管理员");
    }
}
