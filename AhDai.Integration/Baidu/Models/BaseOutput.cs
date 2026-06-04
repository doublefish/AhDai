using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// BaseOutput
/// </summary>
public class BaseOutput : IBaseOutput
{
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonPropertyName("error_description")]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public void EnsureResult()
    {
        if (!string.IsNullOrEmpty(Error)) throw new Exception($"请求百度云服务发生异常：[{Error}]{ErrorMessage}，请联系管理员");
    }
}
