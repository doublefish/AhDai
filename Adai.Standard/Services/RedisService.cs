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
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public RedisService(IConfiguration configuration)
		{
			var config = new Redis.Config()
			{
				Host = configuration.GetSection("redis:host").Value,
				Port = configuration.GetSection("redis:port").Value.ToInt32(),
				Password = configuration.GetSection("redis:password").Value
			};
			Redis.Helper.Init(config);
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <returns></returns>
		public IDatabase GetDatabase(int db = -1, object asyncState = null)
		{
			return Redis.Helper.GetDatabase(db, asyncState);
		}
	}
}
