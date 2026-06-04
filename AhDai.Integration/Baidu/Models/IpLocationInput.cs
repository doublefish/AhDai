using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// IpLocationInput
/// </summary>
public class IpLocationInput : BaseMapInput
{
    /// <summary>
    /// 用户上网的IP地址，请求中如果不出现或为空，会针对发来请求的IP进行定位。 如您需要通过IPv6来获取位置信息，请提交工单申请。
    /// </summary>
    [Required]
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }
    /// <summary>
    /// 设置返回位置信息中，经纬度的坐标类型，分别如下： coor不出现、或为空：百度墨卡托坐标，即百度米制坐标 coor = bd09ll：百度经纬度坐标，在国测局坐标基础之上二次加密而来 coor = gcj02：国测局02坐标，在原始GPS坐标基础上，按照国家测绘行业统一要求，加密后的坐标 注意：百度地图的坐标类型为bd09ll，如果结合百度地图使用，请注意坐标选择
    /// </summary>
    [JsonPropertyName("coor")]
    public string? Coor { get; set; }
}
