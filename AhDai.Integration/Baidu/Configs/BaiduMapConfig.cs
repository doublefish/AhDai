using AhDai.Integration.Attributes;

namespace AhDai.Integration.Baidu.Configs;

/// <summary>
/// BaiduMapConfig
/// </summary>
public class BaiduMapConfig : BaseConfig
{
    /// <summary>
    /// AccessKey
    /// </summary>
    [Sensitive]
    public string AccessKey { get; set; } = default!;
}
