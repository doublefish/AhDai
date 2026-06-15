using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models.Sms;

/// <summary>
/// BaseOutput
/// </summary>
public abstract class BaseOutput : IBaseOutput
{
    /// <summary>
    /// 请求状态码，返回 OK 代表请求成功
    /// </summary>
    [JsonPropertyName("Code")]
    public virtual string Code { get; set; } = default!;
    /// <summary>
    /// 状态码的描述
    /// </summary>
    [JsonPropertyName("Message")]
    public virtual string Message { get; set; } = default!;
    /// <summary>
    /// 请求ID
    /// </summary>
    [JsonPropertyName("RequestId")]
    public virtual string RequestId { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Code != "OK") throw new Exception($"请求阿里云短信发生异常：[{Code}]{Message}，请联系管理员");
    }
}
