using Adai.Standard.Extensions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Adai.Standard.Services
{
	/// <summary>
	/// Redis服务
	/// </summary>
	public class RedisService : Interfaces.IRedisService
	{
		/// <summary>
		/// 配置
		/// </summary>
		public Models.RedisConfig Config { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public RedisService(IConfiguration configuration)
		{
			Config = configuration.GetRedisConfig();
			Utils.RedisHelper.Init(Config);
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="config"></param>
		public RedisService(Models.RedisConfig config)
		{
			Config = config;
			Utils.RedisHelper.Init(Config);
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <returns></returns>
		public IDatabase GetDatabase(int db = -1, object asyncState = null)
		{
			return Utils.RedisHelper.GetDatabase(db, asyncState, Config);
		}
	}
}
