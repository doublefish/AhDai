using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.Aliyun.Configs;

/// <summary>
/// BaseAliyunConfig
/// </summary>
public abstract class BaseAliyunConfig : BaseConfig
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
