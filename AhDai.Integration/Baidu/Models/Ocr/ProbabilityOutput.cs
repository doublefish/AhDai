using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// ProbabilityOutput
/// </summary>
public class ProbabilityOutput
{
    /// <summary>
    /// 行置信度平均值
    /// </summary>
    [JsonPropertyName("average")]
    public decimal Average { get; set; }
    /// <summary>
    /// 行置信度方差
    /// </summary>
    [JsonPropertyName("min")]
    public decimal Min { get; set; }
    /// <summary>
    /// 行置信度最小值
    /// </summary>
    [JsonPropertyName("variance")]
    public decimal Variance { get; set; }
}
