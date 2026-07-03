using StackExchange.Redis;
using System.Threading.Tasks;

namespace AhDai.Core.Infrastructure.MessageBus;

/// <summary>
/// RedisStreamExtensions
/// </summary>
public static class RedisStreamExtensions
{
    /// <summary>
    /// CreateGroupIfNotExistsAsync
    /// </summary>
    /// <param name="db"></param>
    /// <param name="stream"></param>
    /// <param name="group"></param>
    /// <returns></returns>
    public static async Task CreateGroupIfNotExistsAsync(this IDatabase db, string stream, string group)
    {
        try
        {
            await db.StreamCreateConsumerGroupAsync(stream, group, "$", true);
        }
        catch (RedisServerException ex)
        when (ex.Message.StartsWith("BUSYGROUP"))
        {
        }
    }
}
