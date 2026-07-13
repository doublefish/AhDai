using System;
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
    /// 发布
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="topic"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync<T>(string topic, T message, CancellationToken cancellationToken = default);

    /// <summary>
    /// 消费
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="topic"></param>
    /// <param name="group"></param>
    /// <param name="consumer"></param>
    /// <param name="count"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<MessageBusMessage<T>>> ConsumeAsync<T>(string topic, string group, string consumer, int count = 100, CancellationToken cancellationToken = default);

    /// <summary>
    /// 确认回答
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="group"></param>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AckAsync(string topic, string group, IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// 恢复未确认的消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="topic"></param>
    /// <param name="group"></param>
    /// <param name="consumer"></param>
    /// <param name="minIdleTime"></param>
    /// <param name="count"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<MessageBusMessage<T>>> RecoverAsync<T>(string topic, string group, string consumer, TimeSpan minIdleTime, int count, CancellationToken cancellationToken = default);
}
