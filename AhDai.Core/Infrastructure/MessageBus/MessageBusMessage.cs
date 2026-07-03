namespace AhDai.Core.Infrastructure.MessageBus;

/// <summary>
/// MessageBusMessage
/// </summary>
public class MessageBusMessage<T>
{
    /// <summary>
    /// Id
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// Value
    /// </summary>
    public required T Value { get; init; }
}
