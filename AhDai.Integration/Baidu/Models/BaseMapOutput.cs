using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// BaseMapOutput
/// </summary>
public class BaseMapOutput : IBaseOutput
{
    /// <summary>
    /// 返回结果状态值，成功返回0，其他值请查看下方返回码状态表。
    /// </summary>
    [JsonPropertyName("status")]
    public virtual int Status { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    [JsonPropertyName("message")]
    public virtual string? Message { get; set; }

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Status != 0) throw new Exception($"请求百度地图服务发生异常：[{Status}]{Message}，请联系管理员");
    }
}
