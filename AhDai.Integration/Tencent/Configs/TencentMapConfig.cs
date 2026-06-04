using AhDai.Integration.Attributes;

namespace AhDai.Integration.Tencent.Configs;

/// <summary>
/// TencentMapConfig
/// </summary>
public class TencentMapConfig : BaseConfig
{
    /// <summary>
    /// 密钥
    /// </summary>
    [Sensitive]
    public string Key { get; set; } = default!;
}
