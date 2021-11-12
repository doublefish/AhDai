using Adai.Base.Extensions;
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
			Config = new Models.RedisConfig()
			{
				Host = configuration.GetSection("redis:host").Value,
				Port = configuration.GetSection("redis:port").Value.ToInt32(),
				Password = configuration.GetSection("redis:password").Value
			};
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
