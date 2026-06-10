using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.Hikvision.Configs;

/// <summary>
/// BaseHikvisionConfig
/// </summary>
public abstract class BaseHikvisionConfig : BaseConfig
{
    /// <summary>
    /// AppKey
    /// </summary>
    public string AppKey { get; set; } = default!;
    /// <summary>
    /// AppSecret
    /// </summary>
    [Sensitive]
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// 启用数据加密
    /// </summary>
    public bool EnableSecret { get; set; }
}
