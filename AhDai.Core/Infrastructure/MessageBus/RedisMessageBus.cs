using AhDai.Core.Infrastructure.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Infrastructure.MessageBus;

/// <summary>
/// RedisMessageBus
/// </summary>
internal class RedisMessageBus(IBaseRedisService redis) : IMessageBus
{
    readonly IDatabase _db = redis.GetDatabase();

    static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task EnsureConsumerGroupAsync(string topic, string group)
    {
        await _db.CreateGroupIfNotExistsAsync(topic, group);
    }

    public async Task PublishAsync<T>(string topic, T message, CancellationToken cancellationToken = default)
    {
        var values = new NameValueEntry[]
        {
            new("body", JsonSerializer.Serialize(message, JsonOptions)),
            new("created", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
        };
        await _db.StreamAddAsync(topic, values);
    }

    public async Task<IReadOnlyList<MessageBusMessage<T>>> ConsumeAsync<T>(string topic, string group, string consumer, int count = 100, CancellationToken cancellationToken = default)
    {
        var result = await _db.StreamReadGroupAsync(topic, group, consumer, ">", count);

        if (result.Length == 0) return [];

        var messages = new List<MessageBusMessage<T>>(result.Length);
        foreach (var entry in result)
        {
            var body = entry["body"].ToString();
            messages.Add(new MessageBusMessage<T>()
            {
                Id = entry.Id!,
                Value = JsonSerializer.Deserialize<T>(body, JsonOptions)!
            });
        }
        return messages;
    }

    public async Task AckAsync(string stream, string group, IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default)
    {
        if (ids.Count == 0) return;
        var values = ids.Select(x => (RedisValue)x).ToArray();
        await _db.StreamAcknowledgeAsync(stream, group, values);
    }

    public async Task<IReadOnlyList<MessageBusMessage<T>>> RecoverAsync<T>(string topic, string group, string consumer, TimeSpan minIdleTime,  int count, CancellationToken cancellationToken = default)
    {
        var result = await _db.StreamAutoClaimAsync(topic, group, consumer, (long)minIdleTime.TotalMilliseconds, "0-0", count);

        var messages = new List<MessageBusMessage<T>>();
        foreach (var entry in result.ClaimedEntries)
        {
            var body = entry.Values.First(x => x.Name == "body").Value.ToString();
            messages.Add(new MessageBusMessage<T>
            {
                Id = entry.Id.ToString(),
                Value = JsonSerializer.Deserialize<T>(body)!
            });
        }
        return messages;
    }
}
