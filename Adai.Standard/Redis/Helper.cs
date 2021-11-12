using StackExchange.Redis;
using System.Collections.Generic;

namespace Adai.Standard.Redis
{
	/// <summary>
	/// RedisHelper
	/// </summary>
	public static class Helper
	{
		static object Locker { get; set; }

		/// <summary>
		/// DbCount
		/// </summary>
		public const int DbCount = 16;

		/// <summary>
		/// 连接实例
		/// </summary>
		public static IDictionary<string, IConnectionMultiplexer> Instances { get; private set; }

		/// <summary>
		/// Config
		/// </summary>
		public static Config Config { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		static Helper()
		{
			Locker = new object();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="config"></param>
		public static void Init(Config config)
		{
			Config = config;
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

		/// <summary>
		/// CreateConfiguration
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static string CreateConfiguration(Config config)
		{
			return CreateConfiguration(config.Host, config.Port, config.Password);
		}

		/// <summary>
		/// 获取连接实例
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IConnectionMultiplexer GetConnectionMultiplexer(Config config = null)
		{
			if (config == null)
			{
				config = Config;
			}
			var str = CreateConfiguration(config);
			lock (Locker)
			{
				if (Instances == null)
				{
					Instances = new Dictionary<string, IConnectionMultiplexer>();
				}
				if (!Instances.TryGetValue(str, out var instance) || !instance.IsConnected)
				{
					instance = ConnectionMultiplexer.Connect(str);

					// 注册事件
					instance.ConfigurationChangedBroadcast += config.ConfigurationChangedBroadcast;
					instance.ConfigurationChanged += config.ConfigurationChanged;
					instance.HashSlotMoved += config.HashSlotMoved;
					instance.ErrorMessage += config.ErrorMessage;
					instance.InternalError += config.InternalError;
					instance.ConnectionFailed += config.ConnectionFailed;
					instance.ConnectionRestored += config.ConnectionRestored;
					Instances.Add(str, instance);
				}
				return instance;
			}
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(int db = -1, object asyncState = null, Config config = null)
		{
			if (db == -1 && config != null)
			{
				db = config.Database;
			}
			var multiplexer = GetConnectionMultiplexer(config);
			return multiplexer.GetDatabase(db, asyncState);
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(Config config = null)
		{
			return GetDatabase(-1, null, config);
		}
	}
}
