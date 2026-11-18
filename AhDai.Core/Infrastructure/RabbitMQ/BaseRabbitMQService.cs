using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Infrastructure.RabbitMQ;

/// <summary>
/// RabbitMQ
/// </summary>
/// <param name="logger"></param>
/// <param name="options"></param>
internal class BaseRabbitMQService(ILogger<BaseRabbitMQService> logger, IOptions<RabbitMQOptions> options) : IRabbitMQService, IDisposable, IAsyncDisposable
{
    readonly ILogger<BaseRabbitMQService> _logger = logger;
    readonly RabbitMQOptions _options = options.Value;
    readonly ConcurrentDictionary<string, Lazy<Task<IConnection>>> _connections = new(StringComparer.Ordinal);

    int _disposed;

    /// <summary>
    /// 创建连接工厂
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    public virtual ConnectionFactory CreateConnectionFactory(RabbitMQOptions? options = null)
    {
        var ops = options ?? _options;

        return new ConnectionFactory
        {
            HostName = ops.HostName,
            Port = ops.Port,
            UserName = ops.UserName,
            Password = ops.Password,
            VirtualHost = ops.VirtualHost,

            ClientProvidedName = ops.ClientProvidedName,

            AutomaticRecoveryEnabled = ops.AutomaticRecoveryEnabled,
            TopologyRecoveryEnabled = ops.TopologyRecoveryEnabled,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(Math.Max(1, ops.NetworkRecoveryIntervalSeconds))
        };
    }

    /// <summary>
    /// 创建独立连接
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<IConnection> CreateConnectionAsync(RabbitMQOptions? options = null, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        var ops = options ?? _options;
        var factory = CreateConnectionFactory(ops);

        try
        {
            var connection = await factory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false);

            RegisterConnectionEvents(connection, ops);

            if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("RabbitMQ 连接成功：HostName={HostName}, Port={Port}, VirtualHost={VirtualHost}", ops.HostName, ops.Port, ops.VirtualHost);

            return connection;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RabbitMQ 连接失败：HostName={HostName}, Port={Port}, VirtualHost={VirtualHost}", ops.HostName, ops.Port, ops.VirtualHost);

            throw;
        }
    }

    /// <summary>
    /// 获取连接实例
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<IConnection> GetConnectionAsync(
        RabbitMQOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        var ops = CopyOptions(options ?? _options);
        var key = CreateConnectionKey(ops);

        var lazy = _connections.GetOrAdd(key, _ => new Lazy<Task<IConnection>>(() => CreateConnectionAsync(ops, CancellationToken.None), LazyThreadSafetyMode.ExecutionAndPublication));

        var connectionTask = lazy.Value;

        try
        {
            // 这里只取消当前调用者的等待，不取消共享连接的创建
            return await connectionTask.WaitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            // 连接初始化失败后移除缓存，下次调用允许重新创建
            if (connectionTask.IsFaulted || connectionTask.IsCanceled)
            {
                ((ICollection<KeyValuePair<string, Lazy<Task<IConnection>>>>)_connections).Remove(new KeyValuePair<string, Lazy<Task<IConnection>>>(key, lazy));
            }

            throw;
        }
    }

    /// <summary>
    /// 创建 Channel
    /// </summary>
    /// <param name="options">自定义连接配置</param>
    /// <param name="channelOptions">Channel 配置</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<IChannel> CreateChannelAsync(RabbitMQOptions? options = null, CreateChannelOptions? channelOptions = null, CancellationToken cancellationToken = default)
    {
        var connection = await GetConnectionAsync(options, cancellationToken).ConfigureAwait(false);

        return await connection.CreateChannelAsync(channelOptions, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 创建连接缓存 Key
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    protected virtual string CreateConnectionKey(RabbitMQOptions options)
    {
        var value = string.Join("|",
            options.HostName,
            options.Port.ToString(CultureInfo.InvariantCulture),
            options.UserName,
            options.Password,
            options.VirtualHost,
            options.ClientProvidedName ?? string.Empty,
            options.AutomaticRecoveryEnabled,
            options.TopologyRecoveryEnabled,
            options.NetworkRecoveryIntervalSeconds);

        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }

    /// <summary>
    /// 复制配置，避免缓存期间外部修改配置对象
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    protected virtual RabbitMQOptions CopyOptions(RabbitMQOptions options)
    {
        return new RabbitMQOptions
        {
            HostName = options.HostName,
            Port = options.Port,
            UserName = options.UserName,
            Password = options.Password,
            VirtualHost = options.VirtualHost,
            ClientProvidedName = options.ClientProvidedName,
            AutomaticRecoveryEnabled = options.AutomaticRecoveryEnabled,
            TopologyRecoveryEnabled = options.TopologyRecoveryEnabled,
            NetworkRecoveryIntervalSeconds = options.NetworkRecoveryIntervalSeconds
        };
    }

    /// <summary>
    /// 注册连接事件
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="options"></param>
    protected virtual void RegisterConnectionEvents(IConnection connection, RabbitMQOptions options)
    {
        connection.ConnectionShutdownAsync += (_, e) =>
        {
            if (_logger.IsEnabled(LogLevel.Debug)) _logger.LogDebug("RabbitMQ 连接关闭：HostName={HostName}, VirtualHost={VirtualHost}, ReplyCode={ReplyCode}, ReplyText={ReplyText}", options.HostName, options.VirtualHost, e.ReplyCode, e.ReplyText);
            return Task.CompletedTask;
        };

        connection.RecoverySucceededAsync += (_, _) =>
        {
            if (_logger.IsEnabled(LogLevel.Information)) _logger.LogInformation("RabbitMQ 连接恢复成功：HostName={HostName}, VirtualHost={VirtualHost}", options.HostName, options.VirtualHost);
            return Task.CompletedTask;
        };
    }

    /// <summary>
    /// 检查服务是否已释放
    /// </summary>
    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public virtual void Dispose()
    {
        DisposeAsync().AsTask().GetAwaiter().GetResult();
    }

    /// <summary>
    /// DisposeAsync
    /// </summary>
    public virtual async ValueTask DisposeAsync()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
        {
            return;
        }

        var tasks = _connections.Values.Where(x => x.IsValueCreated).Select(x => x.Value).ToArray();

        _connections.Clear();

        foreach (var task in tasks)
        {
            try
            {
                var connection = await task.ConfigureAwait(false);
                await connection.DisposeAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "释放 RabbitMQ 连接时发生异常");
            }
        }

        GC.SuppressFinalize(this);
    }
}
