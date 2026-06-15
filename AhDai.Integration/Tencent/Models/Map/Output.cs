using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models.Map;

/// <summary>
/// Output
/// </summary>
/// <typeparam name="T"></typeparam>
public class Output<T> : IBaseOutput
{
    /// <summary>
    /// 状态码，0为正常，其它为异常
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }
    /// <summary>
    /// 状态说明
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;
    /// <summary>
    /// 本次请求的唯一标识
    /// </summary>
    [JsonPropertyName("request_id")]
    public string RequestId { get; set; } = default!;
    /// <summary>
    /// 结果
    /// </summary>
    [JsonPropertyName("result")]
    public T? Result { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public void EnsureResult()
    {
        if (Status != 0) throw new Exception($"请求腾讯地图发生异常：[{Status}]{Message}，请联系管理员");
    }
}
