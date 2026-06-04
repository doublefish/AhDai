using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 逆地理编码
/// <see href="https://baidumap.apifox.cn/api-32790722">详细文档请参阅</see>
/// </summary>
public class ReverseGeocodingOutput : BaseMapOutput
{
    /// <summary>
    /// 结果
    /// </summary>
    [JsonPropertyName("result")]
    public virtual ReverseGeoCodingResultOutput Result { get; set; } = default!;
}