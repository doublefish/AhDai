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
}
