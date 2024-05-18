using AhDai.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace AhDai.Service.Impl.Base;

/// <summary>
/// 构造函数
/// </summary>
/// <param name="logger"></param>
internal class BaseMessageQueueServiceImpl(ILogger<BaseMessageQueueServiceImpl> logger) : BaseServiceImpl(logger)
{
	protected const string USER_ID = "X-User-Id";
	protected const string USER_USERNAME = "X-User-Username";
	protected const string USER_NAME = "X-User-Name";
	protected const string ROLE_CODES = "X-Role-Codes";
	protected const string EMPLOYEE_ID = "X-Employee-Id";
	protected const string ORG_ID = "X-Org-Id";
	protected const string TENANT_ID = "X-Tenant-Id";
	protected const string TENANT_NAME = "X-Tenant-Name";
	protected const string TENANT_TYPE = "X-Tenant-Type";

	protected IBaseRedisService RedisService = MyApp.Services.GetRequiredService<IBaseRedisService>();

	protected ISubscriber GetSubscriber()
	{
		var redis = RedisService.GetConnectionMultiplexer();
		return redis.GetSubscriber();
	}

}
