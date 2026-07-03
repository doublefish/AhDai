using AhDai.Core.Infrastructure.Redis;
using StackExchange.Redis;
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

    public async Task EnsureConsumerGroupAsync(string stream, string group)
    {
        await _db.CreateGroupIfNotExistsAsync(stream, group);
    }

    public async Task PublishAsync<T>(string stream, T message, CancellationToken cancellationToken = default)
    {
        await _db.StreamAddAsync(stream, [new NameValueEntry("body", JsonSerializer.Serialize(message, JsonOptions))]);
    }

    public async Task<IReadOnlyList<MessageBusMessage<T>>> ConsumeAsync<T>(string stream, string group, string consumer, int count = 100, CancellationToken cancellationToken = default)
    {
        var result = await _db.StreamReadGroupAsync(stream, group, consumer, ">", count);

        if (result.Length == 0)
            return [];

        var list = new List<MessageBusMessage<T>>(result.Length);

        foreach (var item in result)
        {
            var body = item["body"].ToString();
            list.Add(new MessageBusMessage<T>()
            {
                Id = item.Id!,
                Value = JsonSerializer.Deserialize<T>(body, JsonOptions)!
            });
        }

        return list;
    }

    public async Task AckAsync(string stream, string group, IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default)
    {
        if (ids.Count == 0) return;
        var values = ids.Select(x => (RedisValue)x).ToArray();
        await _db.StreamAcknowledgeAsync(stream, group, values);
    }
}
