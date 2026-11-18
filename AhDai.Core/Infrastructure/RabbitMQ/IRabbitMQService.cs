using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Infrastructure.RabbitMQ;

/// <summary>
/// IRabbitMQService
/// </summary>
public interface IRabbitMQService
{
    /// <summary>
    /// 创建连接工厂
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    ConnectionFactory CreateConnectionFactory(RabbitMQOptions? options = null);

    /// <summary>
    /// 创建独立连接
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IConnection> CreateConnectionAsync(RabbitMQOptions? options = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取长期复用的连接，连接由服务管理和释放
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IConnection> GetConnectionAsync(RabbitMQOptions? options = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 创建 Channel，服务管理的连接，调用者负责释放 Channel
    /// </summary>
    /// <param name="options">自定义连接配置</param>
    /// <param name="channelOptions">Channel 配置</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IChannel> CreateChannelAsync(RabbitMQOptions? options = null, CreateChannelOptions? channelOptions = null, CancellationToken cancellationToken = default);
}
