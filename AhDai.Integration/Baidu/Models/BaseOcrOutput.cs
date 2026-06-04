using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// BaseOcrOutput
/// </summary>
/// <typeparam name="TWordsResult"></typeparam>
public abstract class BaseOcrOutput<TWordsResult> : IBaseOutput
{
    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("error_code")]
    public virtual int? ErrorCode { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    [JsonPropertyName("error_msg")]
    public virtual string? ErrorMessage { get; set; }
    /// <summary>
    /// 调用的日志id
    /// </summary>
    [JsonPropertyName("log_id")]
    public virtual ulong LogId { get; set; }

    /// <summary>
    /// 识别结果
    /// </summary>
    [JsonPropertyName("words_result")]
    public virtual TWordsResult? WordsResult { get; set; }
    /// <summary>
    /// 识别结果数，表示words_result的元素个数
    /// </summary>
    [JsonPropertyName("words_result_num")]
    public virtual uint WordsResultNum { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (ErrorCode.HasValue) throw new Exception($"请求百度云服务发生异常：[{ErrorCode}]{ErrorMessage}，请联系管理员");
    }
}
