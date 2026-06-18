using AhDai.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;

namespace AhDai.Core.Services;

/// <summary>
/// Redis服务
/// </summary>
public class BaseRedisService : IBaseRedisService, IDisposable
{
    readonly ILogger<BaseRedisService> _logger;
    readonly IOptionsMonitor<Options.RedisOptions> _options;
    readonly ConcurrentDictionary<string, Lazy<IConnectionMultiplexer>> _connections;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="options"></param>
    public BaseRedisService(ILogger<BaseRedisService> logger, IOptionsMonitor<Options.RedisOptions> options)
    {
        _logger = logger;
        _options = options;
        _connections = [];

        _options.OnChange(o =>
        {
            _logger.LogInformation("Redis config changed, disposing old connections");

            var old = _connections.ToArray();
            _connections.Clear();

            foreach (var kv in old)
            {
                try
                {
                    if (kv.Value.IsValueCreated) kv.Value.Value.Dispose();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Dispose failed");
                }
            }
        });
    }

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
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    public virtual string CreateConfiguration(Options.RedisOptions? options = null)
    {
        var o = options ?? _options.CurrentValue;
        return CreateConfiguration(o.Host, o.Port, o.Password, o.AbortConnect);
    }

    /// <summary>
    /// 创建连接器
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <returns></returns>
    public virtual IConnectionMultiplexer CreateConnection(string configuration)
    {
        var options = ConfigurationOptions.Parse(configuration);
        options.AbortOnConnectFail = false;
        var connection = ConnectionMultiplexer.Connect(options);
        // 注册事件
        connection.ConfigurationChangedBroadcast += ConfigurationChangedBroadcast;
        connection.ConfigurationChanged += ConfigurationChanged;
        connection.HashSlotMoved += HashSlotMoved;
        connection.ErrorMessage += ErrorMessage;
        connection.InternalError += InternalError;
        connection.ConnectionFailed += ConnectionFailed;
        connection.ConnectionRestored += ConnectionRestored;
        return connection;
    }

    /// <summary>
    /// 创建连接器
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    public virtual IConnectionMultiplexer CreateConnection(Options.RedisOptions? options = null)
    {
        var o = options ?? _options.CurrentValue;
        var configString = CreateConfiguration(o);
        return CreateConnection(configString);
    }

    /// <summary>
    /// 获取连接实例
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    public virtual IConnectionMultiplexer GetConnection(Options.RedisOptions? options = null)
    {
        var o = options ?? _options.CurrentValue;
        var configString = CreateConfiguration(o);
        var key = $"{o.Host}:{o.Port}:{o.Password}:{o.AbortConnect}";
        var lazy = _connections.GetOrAdd(key, _ => new Lazy<IConnectionMultiplexer>(() =>
        {
            return CreateConnection(configString);
        }, true));
        var connection = lazy.Value;
        //if (!connection.IsConnected)
        //{
        //    _logger.LogWarning("Redis disconnected, recreating...");
        //    _connections.TryRemove(key, out _);
        //    lazy = _connections.GetOrAdd(key, _ => new Lazy<IConnectionMultiplexer>(() =>
        //    {
        //        return CreateConnection(configString);
        //    }, true));
        //    connection = lazy.Value;
        //}
        return connection;
    }

    /// <summary>
    /// GetDatabase
    /// </summary>
    /// <param name="db"></param>
    /// <param name="asyncState"></param>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    public virtual IDatabase GetDatabase(int db = -1, object? asyncState = null, Options.RedisOptions? options = null)
    {
        var o = options ?? _options.CurrentValue;
        var connection = GetConnection(o);
        return connection.GetDatabase(db > -1 ? db : o.Database, asyncState);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public virtual void Dispose()
    {
        foreach (var item in _connections.Values)
        {
            if (item.IsValueCreated)
            {
                item.Value.Dispose();
            }
        }
        _connections.Clear();
        GC.SuppressFinalize(this);
    }

    #region 接收通知事件
    /// <summary>
    /// 当节点被明确请求通过广播重新配置时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConfigurationChangedBroadcast(object? sender, EndPointEventArgs e)
    {
        if (_logger.IsEnabled(LogLevel.Debug)) _logger.LogDebug("ConfigurationChangedBroadcast=>EndPoint={EndPoint}", e.EndPoint);
    }

    /// <summary>
    /// 检测到配置更改时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConfigurationChanged(object? sender, EndPointEventArgs e)
    {
        if (_logger.IsEnabled(LogLevel.Debug)) _logger.LogDebug("ConfigurationChanged=>EndPoint={EndPoint}", e.EndPoint);
    }

    /// <summary>
    /// 当哈希槽被重新定位时/更改集群
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void HashSlotMoved(object? sender, HashSlotMovedEventArgs e)
    {
        if (_logger.IsEnabled(LogLevel.Debug)) _logger.LogDebug("HashSlotMoved=>NewEndPoint={NewEndPoint},OldEndPoint={OldEndPoint}", e.NewEndPoint, e.OldEndPoint);
    }

    /// <summary>
    /// 发生错误时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ErrorMessage(object? sender, RedisErrorEventArgs e)
    {
        if (_logger.IsEnabled(LogLevel.Error)) _logger.LogError("ErrorMessage");
    }

    /// <summary>
    /// 发生内部错误时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void InternalError(object? sender, InternalErrorEventArgs e)
    {
        if (_logger.IsEnabled(LogLevel.Error)) _logger.LogError(e.Exception, "InternalError");
    }

    /// <summary>
    /// 连接失败，如果重新连接成功将不会收到这个通知
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConnectionFailed(object? sender, ConnectionFailedEventArgs e)
    {
        if (_logger.IsEnabled(LogLevel.Debug)) _logger.LogDebug(e.Exception, "ConnectionFailed=>EndPoint={EndPoint},FailureType={FailureType},Message={Message}", e.EndPoint, e.FailureType, e.Exception?.Message);
    }

    /// <summary>
    /// 建立物理连接时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ConnectionRestored(object? sender, ConnectionFailedEventArgs e)
    {
        if (_logger.IsEnabled(LogLevel.Debug)) _logger.LogDebug("ConnectionRestored=>EndPoint={EndPoint}", e.EndPoint);
    }
    #endregion
}
