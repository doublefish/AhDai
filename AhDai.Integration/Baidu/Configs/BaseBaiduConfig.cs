using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.Baidu.Configs;

/// <summary>
/// BaseBaiduConfig
/// </summary>
public abstract class BaseBaiduConfig : BaseConfig
{
    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// ApiKey
    /// </summary>
    public string ApiKey { get; set; } = default!;
    /// <summary>
    /// AppSecret
    /// </summary>
    [Sensitive]
    public string AppSecret { get; set; } = default!;
}
