using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Infrastructure.MessageBus;

/// <summary>
/// IMessageBus
/// </summary>
public interface IMessageBus
{
    /// <summary>
    /// 确保消费者组已创建
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="group"></param>
    Task EnsureConsumerGroupAsync(string stream, string group);

    /// <summary>
    /// 发布消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync<T>(string stream, T message, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量消费
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream"></param>
    /// <param name="group"></param>
    /// <param name="consumer"></param>
    /// <param name="count"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<MessageBusMessage<T>>> ConsumeAsync<T>(string stream, string group, string consumer, int count = 100, CancellationToken cancellationToken = default);

    /// <summary>
    /// ACK
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="group"></param>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AckAsync(string stream, string group, IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default);
}
