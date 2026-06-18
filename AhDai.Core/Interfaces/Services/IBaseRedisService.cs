using StackExchange.Redis;

namespace AhDai.Core.Interfaces.Services;

/// <summary>
/// Redis服务
/// </summary>
public interface IBaseRedisService
{
    /// <summary>
    /// 创建连接配置
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="password"></param>
    /// <param name="abortConnect"></param>
    /// <returns></returns>
    string CreateConfiguration(string host, int port = 6379, string? password = null, bool abortConnect = false);

    /// <summary>
    /// 创建连接器
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <returns></returns>
    IConnectionMultiplexer CreateConnection(string configuration);

    /// <summary>
    /// 创建连接配置
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    string CreateConfiguration(Options.RedisOptions? options = null);

    /// <summary>
    /// 创建连接器
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    IConnectionMultiplexer CreateConnection(Options.RedisOptions? options = null);

    /// <summary>
    /// 获取连接实例
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    IConnectionMultiplexer GetConnection(Options.RedisOptions? options = null);

    /// <summary>
    /// GetDatabase
    /// </summary>
    /// <param name="db"></param>
    /// <param name="asyncState"></param>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    IDatabase GetDatabase(int db = -1, object? asyncState = null, Options.RedisOptions? options = null);
}
