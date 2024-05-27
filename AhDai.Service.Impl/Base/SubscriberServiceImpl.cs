using AhDai.Base.Extensions;
using AhDai.Core.Utils;
using AhDai.Service.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Base;

/// <summary>
/// 订阅者服务
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class SubscriberServiceImpl(ILogger<SubscriberServiceImpl> logger)
    : BaseMessageQueueServiceImpl(logger)
    , ISubscriberService
{
    public async Task SubscribeAsync<T>(string channel, Action<string, T> handler)
    {
        var subscriber = GetSubscriber();
        await subscriber.SubscribeAsync(RedisChannel.Literal(channel), (channel, message) =>
        {
            var channelString = channel.ToString();
            var messageString = message.ToString();
            try
            {
                var data = JsonUtil.Deserialize<MessageData<T>>(channelString) ?? throw new Exception("反序列化消息发生异常");
                if (data.Headers.TryGetValue(USER_ID, out var userId))
                {
                    data.Headers.TryGetValue(USER_USERNAME, out var username);
                    data.Headers.TryGetValue(USER_NAME, out var name);
                    data.Headers.TryGetValue(ROLE_CODES, out var roleCodes);
                    data.Headers.TryGetValue(EMPLOYEE_ID, out var employeeId);
                    data.Headers.TryGetValue(ORG_ID, out var orgId);
                    data.Headers.TryGetValue(TENANT_ID, out var tenantId);
                    data.Headers.TryGetValue(TENANT_NAME, out var tenantName);
                    data.Headers.TryGetValue(TENANT_TYPE, out var tenantType);
                    MyApp.SetOperatingUser(new Models.LoginData()
                    {
                        Id = userId.ToInt64(0),
                        Username = username ?? "",
                        Name = name ?? "",
                        RoleCodes = Utils.ModelUtils.ToArray(roleCodes),
                        EmployeeId = employeeId.ToInt64(0),
                        OrgId = orgId.ToInt64(0),
                        TenantId = tenantId.ToInt64(0),
                        TenantName = tenantName ?? "",
                        TenantType = (Shared.Enums.TenantType)tenantType.ToInt32(0),
                    });
                }
                handler.Invoke(channelString, data.Body);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "订阅消息异常=>{Message}\r\n{channel} {message}", ex.Message, channelString, messageString);
                throw;
            }

        });
    }


}
