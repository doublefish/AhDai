using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.ESign.Configs;

/// <summary>
/// ESignConfig
/// </summary>
public class ESignConfig : BaseConfig
{
    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// AppSecret
    /// </summary>
    [Sensitive]
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// NotifyUrl
    /// </summary>
    public string NotifyUrl { get; set; } = default!;
}
