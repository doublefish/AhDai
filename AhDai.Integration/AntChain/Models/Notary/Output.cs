using AhDai.Integration.Abstractions;
using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// Output
/// </summary>
/// <typeparam name="T"></typeparam>
public class Output<T> : IBaseOutput
{
    /// <summary>
    /// 响应
    /// </summary>
    [JsonPropertyName("response")]
    public T? Response { get; set; }
    /// <summary>
    /// 签名
    /// </summary>
    [JsonPropertyName("sign")]
    public string? Sign { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Response == null) throw new Exception($"请求蚂蚁连存证发生异常：返回数据为空，请联系管理员");
    }
}
