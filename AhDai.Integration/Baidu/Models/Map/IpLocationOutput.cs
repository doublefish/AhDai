using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Map;

/// <summary>
/// IpLocationOutput
/// </summary>
public class IpLocationOutput : BaseOutput
{
    /// <summary>
    /// 详细地址信息
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;
    /// <summary>
    /// 内容
    /// </summary>
    [JsonPropertyName("content")]
    public IpLocationContentOutput Content { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public override void EnsureResult()
    {
        base.EnsureResult();
        if (string.IsNullOrEmpty(Address) || Content == null) throw new Exception($"请求百度地图发生异常：返回数据为空，请联系管理员");
    }
}
