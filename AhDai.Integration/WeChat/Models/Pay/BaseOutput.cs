using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// Output
/// </summary>
public abstract class BaseOutput : IBaseOutput
{
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("code")]
    public virtual string? Code { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonPropertyName("message")]
    public virtual string? Message { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (!string.IsNullOrEmpty(Code)) throw new Exception($"请求微信支付发生异常：[{Code}]{Message}，请联系管理员");
    }
}
