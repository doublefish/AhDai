using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Map;

/// <summary>
/// 周边poi数组
/// </summary>
public class PoiOutput
{
    /// <summary>
    /// 地址信息
    /// </summary>
    [JsonPropertyName("addr")]
    public string Addr { get; set; } = default!;
    /// <summary>
    /// 数据来源（已废弃）
    /// </summary>
    [JsonPropertyName("cp")]
    public string Cp { get; set; } = default!;
    /// <summary>
    /// 和当前坐标点的方向
    /// </summary>
    [JsonPropertyName("direction")]
    public string Direction { get; set; } = default!;
    /// <summary>
    /// 离坐标点距离
    /// </summary>
    [JsonPropertyName("distance")]
    public int Distance { get; set; }
    /// <summary>
    /// poi名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// poi类型，如’美食;中餐厅’。tag与poiType字段均为poi类型，建议使用tag字段，信息更详细。poi详细类别
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = default!;
    /// <summary>
    /// poi坐标{x,y}
    /// </summary>
    [JsonPropertyName("point")]
    public string Point { get; set; } = default!;
    /// <summary>
    /// 电话
    /// </summary>
    [JsonPropertyName("tel")]
    public int Tel { get; set; }
    /// <summary>
    /// uid
    /// </summary>
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = default!;
    /// <summary>
    /// 邮编
    /// </summary>
    [JsonPropertyName("zip")]
    public int Zip { get; set; }
    /// <summary>
    /// poi对应的主点poi（如，海底捞的主点为上地华联，该字段则为上地华联的poi信息。如无，该字段为空），包含子字段和pois基础召回字段相同。
    /// </summary>
    [JsonPropertyName("parent_poi")]
    public string ParentPoi { get; set; } = default!;
}
