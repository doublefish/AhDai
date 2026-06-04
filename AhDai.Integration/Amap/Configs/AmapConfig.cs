using AhDai.Integration.Attributes;

namespace AhDai.Integration.Amap.Configs;

/// <summary>
/// AmapConfig
/// </summary>
public class AmapConfig : BaseConfig
{
    /// <summary>
    /// AppSecret
    /// </summary>
    [Sensitive]
    public string AccessKey { get; set; } = default!;
}
