using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models;

/// <summary>
/// BaseSmsOutput
/// </summary>
public class SmsOutput : IBaseOutput
{
    /// <summary>
    /// 请求状态码，返回 OK 代表请求成功
    /// </summary>
    [JsonPropertyName("Code")]
    public string Code { get; set; } = default!;
    /// <summary>
    /// 状态码的描述
    /// </summary>
    [JsonPropertyName("Message")]
    public string Message { get; set; } = default!;
    /// <summary>
    /// 发送回执ID
    /// </summary>
    [JsonPropertyName("BizId")]
    public string BizId { get; set; } = default!;
    /// <summary>
    /// 请求ID
    /// </summary>
    [JsonPropertyName("RequestId")]
    public string RequestId { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Code != "OK") throw new Exception($"请求阿里云短信发生异常：[{Code}]{Message}，请联系管理员");
    }
}
