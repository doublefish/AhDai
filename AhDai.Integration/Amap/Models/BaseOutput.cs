using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// BaseOutput
/// </summary>
public class BaseOutput : IBaseOutput
{
    /// <summary>
    /// 结果状态值：值为0或1，0表示失败；1表示成功
    /// </summary>
    [JsonPropertyName("status")]
    public virtual int Status { get; set; }
    /// <summary>
    /// 状态说明：status 为0时，info 返回错误原因，否则返回“OK”。
    /// </summary>
    [JsonPropertyName("info")]
    public virtual string Info { get; set; } = default!;
    /// <summary>
    /// 状态码：10000代表正确,详情参阅 info 状态表
    /// </summary>
    [JsonPropertyName("infocode")]
    public virtual string InfoCode { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (Status != 1) throw new Exception($"请求高德地图服务发生异常：[{InfoCode}]{Info}，请联系管理员");
    }
}
