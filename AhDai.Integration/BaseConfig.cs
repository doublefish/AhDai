namespace AhDai.Integration;

/// <summary>
/// BaseConfig
/// </summary>
public abstract class BaseConfig : IConfig
{
    /// <summary>
    /// Host
    /// </summary>
    public string Host { get; set; } = default!;
}
