using AhDai.Core.Utils;
using AhDai.Service.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Base;

/// <summary>
/// 发布者服务
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class PublisherServiceImpl(ILogger<PublisherServiceImpl> logger)
	: BaseMessageQueueServiceImpl(logger)
	, IPublisherService
{

	public async Task PublishAsync<T>(string channel, T body)
	{
		await PublishAsync(channel, null, body);
	}

	public async Task PublishAsync<T>(string channel, IDictionary<string, string>? headers, T body)
	{
		headers ??= new Dictionary<string, string>();
		try
		{
			var loginData = await MyApp.GetLoginDataAsync();
			headers.Add(USER_ID, loginData.Id.ToString());
			headers.Add(USER_USERNAME, loginData.Username);
			headers.Add(USER_NAME, loginData.Name);
			headers.Add(ROLE_CODES, Utils.ModelUtils.ToString(loginData.RoleCodes));
			headers.Add(EMPLOYEE_ID, loginData.EmployeeId.ToString());
			headers.Add(ORG_ID, loginData.OrgId.ToString());
			headers.Add(TENANT_ID, loginData.TenantId.ToString());
			headers.Add(TENANT_NAME, loginData.TenantName);
			headers.Add(TENANT_TYPE, ((int)loginData.TenantType).ToString());
			var data = new MessageData<T>(headers, body);
			var jsonString = JsonUtil.Serialize(data);
			var subscriber = GetSubscriber();
			await subscriber.PublishAsync(RedisChannel.Literal(channel), new RedisValue(jsonString));
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "发布消息异常=>{Message}\r\n{channel} {body}", ex.Message, channel, body);
			throw;
		}
	}

}
