namespace AhDai.Integration.Abstractions;

/// <summary>
/// IRedisKeyBuilder
/// </summary>
public interface IRedisKeyBuilder
{
    /// <summary>
    /// Build
    /// </summary>
    /// <param name="segments"></param>
    /// <returns></returns>
    string Build(params string[] segments);
}
