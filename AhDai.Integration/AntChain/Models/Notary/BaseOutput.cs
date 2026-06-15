using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// BaseOutput
/// </summary>
public abstract class BaseOutput : IBaseOutput
{
    /// <summary>
    /// API调用结果码，此接口成功为200
    /// </summary>
    [JsonPropertyName("result_code")]
    public virtual string? ResultCode { get; set; }
    /// <summary>
    /// API调用结果描述，比如调用失败的时候会显示具体的错误信息,成功为success
    /// </summary>
    [JsonPropertyName("result_msg")]
    public virtual string? ResultMsg { get; set; }
    /// <summary>
    /// 请求消息id，全链路唯一标记，建议打印，排查问题需提供
    /// </summary>
    [JsonPropertyName("req_msg_id")]
    public virtual string? ReqMsgId { get; set; }
    /// <summary>
    /// 产品实例Id
    /// </summary>
    [JsonIgnore]
    public virtual string ProductInstanceId { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (ResultCode != "200") throw new Exception($"请求蚂蚁链存证发生异常：[{ResultCode}]{ResultMsg}，请联系管理员");
    }
}
