using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// BaseOutput
/// </summary>
public abstract class BaseOutput : IBaseOutput
{
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("errcode")]
    public int? ErrorCode { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonPropertyName("errmsg")]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (ErrorCode.HasValue && ErrorCode != 0) throw new Exception($"请求微信发生异常：[{ErrorCode}]{ErrorMessage}，请联系管理员");
    }
}
