using System.Text.Json.Serialization;

namespace AhDai.Integration.Tencent.Models;

/// <summary>
/// 坐标相对位置参考
/// </summary>
public class AddressReferenceOutput
{
    /// <summary>
    /// 知名区域，如商圈或人们普遍认为有较高知名度的区域
    /// </summary>
    [JsonPropertyName("famous_area")]
    public AreaItemOutput? FamousArea { get; set; }
    /// <summary>
    /// 商圈，目前与famous_area一致
    /// </summary>
    [JsonPropertyName("business_area")]
    public AreaItemOutput? BusinessArea { get; set; }
    /// <summary>
    /// 乡镇/街道（四级行政区划）
    /// </summary>
    [JsonPropertyName("town")]
    public AreaItemOutput? Town { get; set; }
    /// <summary>
    /// 一级地标，可识别性较强、规模较大的地点、小区等
    /// 【注】：对象结构同 famous_area
    /// </summary>
    [JsonPropertyName("landmark_l1")]
    public AreaItemOutput? LandmarkL1 { get; set; }
    /// <summary>
    /// 二级地标，较一级地标更为精确，规模更小
    /// 【注】：对象结构同 famous_area
    /// </summary>
    [JsonPropertyName("landmark_l2")]
    public AreaItemOutput? LandmarkL2 { get; set; }
    /// <summary>
    /// 道路
    /// 【注】：对象结构同 famous_area
    /// </summary>
    [JsonPropertyName("street")]
    public AreaItemOutput? Street { get; set; }
    /// <summary>
    /// 门牌
    /// 【注】：对象结构同 famous_area
    /// </summary>
    [JsonPropertyName("street_number")]
    public AreaItemOutput? StreetNumber { get; set; }
    /// <summary>
    /// 交叉路口
    /// 【注】：对象结构同 famous_area
    /// </summary>
    [JsonPropertyName("crossroad")]
    public AreaItemOutput? Crossroad { get; set; }
    /// <summary>
    /// 水系
    /// 【注】：对象结构同 famous_area
    /// </summary>
    [JsonPropertyName("water")]
    public AreaItemOutput? Water { get; set; }
    /// <summary>
    /// 海洋信息
    /// </summary>
    [JsonPropertyName("ocean")]
    public OceanOutput? Ocean { get; set; }
}
