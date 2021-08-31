using StackExchange.Redis;
using System.Collections.Generic;

namespace Adai.Standard
{
	/// <summary>
	/// RedisHelper
	/// </summary>
	public static class RedisHelper
	{
		/// <summary>
		/// SmptConfiguration
		/// </summary>
		public static Model.RedisConfiguration Configuration { get; private set; }

		/// <summary>
		/// 已初始化
		/// </summary>
		public static bool Initialized => Configuration != null
			&& !string.IsNullOrEmpty(Configuration.Host) && Configuration.Port > 0
			&& !string.IsNullOrEmpty(Configuration.Password);

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static bool Init(Model.RedisConfiguration configuration)
		{
			Configuration = configuration;
			return Initialized;
		}

		/// <summary>
		/// DbCount
		/// </summary>
		public const int DbCount = 16;

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(int db = -1, object asyncState = null)
		{
			return GetConnectionMultiplexer().GetDatabase(db, asyncState);
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(Model.RedisConfiguration configuration = null)
		{
			var multiplexer = GetConnectionMultiplexer(configuration);
			return multiplexer.GetDatabase(configuration.Database);
		}

		/// <summary>
		/// GetConnectionMultiplexer
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IConnectionMultiplexer GetConnectionMultiplexer(Model.RedisConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}
			var str = CreateConfiguration(configuration.Host, configuration.Port, configuration.Password);
			return ConnectionMultiplexer.Connect(str);
		}

		/// <summary>
		/// CreateConfiguration
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public static string CreateConfiguration(string host = "127.0.0.1", int port = 6379, string password = null)
		{
			return $"{host}:{port},password={password}";
		}
	}
}
