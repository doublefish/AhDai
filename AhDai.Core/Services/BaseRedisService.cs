using AhDai.Core.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Collections.Generic;

namespace AhDai.Core.Services;

/// <summary>
/// Redis服务
/// </summary>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public class BaseRedisService(ILogger<BaseRedisService> logger, IConfiguration configuration) : IBaseRedisService
{
    readonly ILogger<BaseRedisService> _logger = logger;
    readonly RedisConfig _config = configuration.GetRedisConfig();
    readonly Dictionary<string, IConnectionMultiplexer> _connectionMultiplexer = [];
    readonly object _locker = new();

    /// <summary>
    /// 配置
    /// </summary>
    public RedisConfig Config => _config;
    /// <summary>
    /// ConnectionMultiplexer
    /// </summary>
    public Dictionary<string, IConnectionMultiplexer> ConnectionMultiplexers => _connectionMultiplexer;

    /// <summary>
    /// 创建连接配置
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="password"></param>
    /// <param name="abortConnect"></param>
    /// <returns></returns>
    public virtual string CreateConfiguration(string host, int port = 6379, string? password = null, bool abortConnect = false)
    {
        return $"{host}:{port},password={password},abortConnect={abortConnect}";
    }

    /// <summary>
    /// 创建连接配置
    /// </summary>
    /// <param name="config">自定义配置</param>
    /// <returns></returns>
    public virtual string CreateConfiguration(RedisConfig? config = null)
    {
        var c = config ?? Config;
        return CreateConfiguration(c.Host, c.Port, c.Password, c.AbortConnect);
    }

    /// <summary>
    /// 创建连接器
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <returns></returns>
    public virtual IConnectionMultiplexer CreateConnectionMultiplexer(string configuration)
    {
        var options = ConfigurationOptions.Parse(configuration);
        var multiplexer = ConnectionMultiplexer.Connect(options);
        // 注册事件
        multiplexer.ConfigurationChangedBroadcast += ConfigurationChangedBroadcast;
        multiplexer.ConfigurationChanged += ConfigurationChanged;
        multiplexer.HashSlotMoved += HashSlotMoved;
        multiplexer.ErrorMessage += ErrorMessage;
        multiplexer.InternalError += InternalError;
        multiplexer.ConnectionFailed += ConnectionFailed;
        multiplexer.ConnectionRestored += ConnectionRestored;
        return multiplexer;
    }



    /// <summary>
    /// 创建连接器
    /// </summary>
    /// <param name="config">自定义配置</param>
    /// <returns></returns>
    public virtual IConnectionMultiplexer CreateConnectionMultiplexer(RedisConfig? config = null)
    {
        var c = config ?? Config;
        var configString = CreateConfiguration(c);
        return CreateConnectionMultiplexer(configString);
    }

    /// <summary>
    /// 获取连接实例
    /// </summary>
    /// <param name="config">自定义配置</param>
    /// <returns></returns>
    public virtual IConnectionMultiplexer GetConnectionMultiplexer(RedisConfig? config = null)
    {
        var c = config ?? Config;
        var configString = CreateConfiguration(c);
        if (!ConnectionMultiplexers.TryGetValue(configString, out var multiplexer) || !multiplexer.IsConnected)
        {
            lock (_locker)
            {
                multiplexer = CreateConnectionMultiplexer(configString);
                ConnectionMultiplexers[configString] = multiplexer;
            }
        }
        return multiplexer;
    }

    /// <summary>
    /// GetDatabase
    /// </summary>
    /// <param name="db"></param>
    /// <param name="asyncState"></param>
    /// <param name="config">自定义配置</param>
    /// <returns></returns>
    public virtual IDatabase GetDatabase(int db = -1, object? asyncState = null, RedisConfig? config = null)
    {
        var c = config ?? Config;
        var multiplexer = GetConnectionMultiplexer(c);
        return multiplexer.GetDatabase(db > -1 ? db : c.Database, asyncState);
    }

    #region 接收通知事件
    /// <summary>
    /// 当节点被明确请求通过广播重新配置时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConfigurationChangedBroadcast(object? sender, EndPointEventArgs e)
    {
        _logger.LogDebug("ConfigurationChangedBroadcast=>EndPoint={EndPoint}", e.EndPoint);
    }

    /// <summary>
    /// 检测到配置更改时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConfigurationChanged(object? sender, EndPointEventArgs e)
    {
        _logger.LogDebug("ConfigurationChanged=>EndPoint={EndPoint}", e.EndPoint);
    }

    /// <summary>
    /// 当哈希槽被重新定位时/更改集群
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void HashSlotMoved(object? sender, HashSlotMovedEventArgs e)
    {
        _logger.LogDebug("HashSlotMoved=>NewEndPoint={NewEndPoint},OldEndPoint={OldEndPoint}", e.NewEndPoint, e.OldEndPoint);
    }

    /// <summary>
    /// 发生错误时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ErrorMessage(object? sender, RedisErrorEventArgs e)
    {
        _logger.LogError("ErrorMessage=>{Message}", e.Message);
    }

    /// <summary>
    /// 发生内部错误时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void InternalError(object? sender, InternalErrorEventArgs e)
    {
        _logger.LogError(e.Exception, "InternalError=>{Message}", e.Exception.Message);
    }

    /// <summary>
    /// 连接失败，如果重新连接成功将不会收到这个通知
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConnectionFailed(object? sender, ConnectionFailedEventArgs e)
    {
        _logger.LogDebug(e.Exception, "ConnectionFailed=>EndPoint={EndPoint},FailureType={FailureType},Message={Message}", e.EndPoint, e.FailureType, e.Exception?.Message);
    }

    /// <summary>
    /// 建立物理连接时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConnectionRestored(object? sender, ConnectionFailedEventArgs e)
    {
        _logger.LogDebug("ConnectionRestored=>EndPoint={EndPoint}", e.EndPoint);
    }
    #endregion
}
