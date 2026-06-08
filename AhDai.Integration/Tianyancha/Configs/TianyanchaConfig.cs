using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.Tianyancha.Configs;

/// <summary>
/// TianyanchaConfig
/// </summary>
public class TianyanchaConfig : BaseConfig
{
    /// <summary>
    /// Key
    /// </summary>
    [Sensitive]
    public string Key { get; set; } = default!;
}
