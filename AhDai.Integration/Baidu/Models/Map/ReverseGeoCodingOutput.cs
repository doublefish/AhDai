using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Map;

/// <summary>
/// 逆地理编码
/// <see href="https://baidumap.apifox.cn/api-32790722">详细文档请参阅</see>
/// </summary>
public class ReverseGeocodingOutput : BaseOutput
{
    /// <summary>
    /// 结果
    /// </summary>
    [JsonPropertyName("result")]
    public virtual ReverseGeoCodingResultOutput Result { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public override void EnsureResult()
    {
        base.EnsureResult();
        if (Result == null) throw new Exception($"请求百度地图发生异常：返回数据为空，请联系管理员");
    }
}