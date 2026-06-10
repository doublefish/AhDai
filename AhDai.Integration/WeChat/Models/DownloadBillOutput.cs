using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// DownloadBillOutput
/// </summary>
public class DownloadBillOutput : IBaseOutput
{
    /// <summary>
    /// 返回状态码
    /// </summary>
    [JsonPropertyName("return_code")]
    public string? ReturnCode { get; set; }
    /// <summary>
    /// 错误码描述	
    /// </summary>
    [JsonPropertyName("return_msg")]
    public string? ReturnMessage { get; set; }
    /// <summary>
    /// 错误码
    /// </summary>
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public void EnsureResult()
    {
        if (!string.IsNullOrEmpty(ReturnCode)) throw new Exception($"请求微信支付发生异常：[{ReturnCode}]{ReturnMessage}，请联系管理员");
    }
}
