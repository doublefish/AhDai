using AhDai.Integration.Attributes;

namespace AhDai.Integration.AntChain.Configs;

/// <summary>
/// AntChainConfig
/// </summary>
public class AntChainConfig : BaseConfig
{
    /// <summary>
    /// AccessKeyId
    /// </summary>
    public string AccessKeyId { get; set; } = default!;
    /// <summary>
    /// AccessKeySecret
    /// </summary>
    [Sensitive]
    public string AccessKeySecret { get; set; } = default!;
}
