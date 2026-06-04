using AhDai.Integration.Attributes;

namespace AhDai.Integration.Aliyun.Configs;

/// <summary>
/// AliyunConfig
/// </summary>
public abstract class AliyunConfig : BaseConfig
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
