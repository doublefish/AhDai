using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Amap.Models;

/// <summary>
/// ReverseGeocodeOutput
/// <see href="https://lbs.amap.com/api/webservice/guide/api/georegeo">详细文档请参阅</see>
/// </summary>
public class ReverseGeocodeOutput : BaseOutput
{
    /// <summary>
    /// 逆地理编码列表
    /// </summary>
    [JsonPropertyName("regeocode")]
    public RegeocodeOutput Regeocode { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    /// <exception cref="Exception"></exception>
    public override void EnsureResult()
    {
        base.EnsureResult();
        if (Regeocode == null) throw new Exception($"请求高德地图发生异常：返回数据为空，请联系管理员");
    }
}
