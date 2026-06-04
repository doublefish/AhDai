using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// BaseMapOutput
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseMapOutput<T> : IBaseOutput
{
    /// <summary>
    /// 状态码，0为正常，其它为异常
    /// </summary>
    [JsonPropertyName("status")]
    public virtual int Status { get; set; }
    /// <summary>
    /// 状态说明
    /// </summary>
    [JsonPropertyName("message")]
    public virtual string Message { get; set; } = default!;
    /// <summary>
    /// 本次请求的唯一标识
    /// </summary>
    [JsonPropertyName("request_id")]
    public virtual string RequestId { get; set; } = default!;
    /// <summary>
    /// 结果
    /// </summary>
    [JsonPropertyName("result")]
    public virtual T Result { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Status != 0) throw new Exception($"请求腾讯地图服务发生异常：[{Status}]{Message}，请联系管理员");
    }
}
