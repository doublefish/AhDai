using StackExchange.Redis;
using System.Collections.Generic;

namespace AhDai.Core.Utils
{
	/// <summary>
	/// RedisHelper
	/// </summary>
	public static class RedisHelper
	{
		static readonly IDictionary<string, IConnectionMultiplexer> ConnectionMultiplexers;
		static readonly object Locker;

		/// <summary>
		/// DbCount
		/// </summary>
		public const int DbCount = 16;

		/// <summary>
		/// 配置
		/// </summary>
		public static Models.RedisConfig Config { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		static RedisHelper()
		{
			ConnectionMultiplexers = new Dictionary<string, IConnectionMultiplexer>();
			Locker = new object();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="config">配置</param>
		public static void Init(Models.RedisConfig config)
		{
			Config = config;
		}

		/// <summary>
		/// 创建连接配置
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="password"></param>
		/// <param name="abortConnect"></param>
		/// <returns></returns>
		public static string CreateConfiguration(string host = "127.0.0.1", int port = 6379, string password = null, bool abortConnect = false)
		{
			return $"{host}:{port},password={password},abortConnect={abortConnect}";
		}

		/// <summary>
		/// 创建连接配置
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static string CreateConfiguration(Models.RedisConfig config = null)
		{
			var c = config ?? Config;
			return CreateConfiguration(c.Host, c.Port, c.Password, c.AbortConnect);
		}

		/// <summary>
		/// 创建连接器
		/// </summary>
		/// <param name="configuration">配置</param>
		/// <returns></returns>
		public static IConnectionMultiplexer CreateConnectionMultiplexer(string configuration)
		{
			var options = ConfigurationOptions.Parse(configuration);
			var multiplexer = ConnectionMultiplexer.Connect(options);
			return multiplexer;
		}

		/// <summary>
		/// 创建连接器
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static IConnectionMultiplexer CreateConnectionMultiplexer(Models.RedisConfig config = null)
		{
			var c = config ?? Config;
			var configString = CreateConfiguration(c);
			var multiplexer = CreateConnectionMultiplexer(configString);
			// 注册事件
			multiplexer.ConfigurationChangedBroadcast += c.ConfigurationChangedBroadcast;
			multiplexer.ConfigurationChanged += c.ConfigurationChanged;
			multiplexer.HashSlotMoved += c.HashSlotMoved;
			multiplexer.ErrorMessage += c.ErrorMessage;
			multiplexer.InternalError += c.InternalError;
			multiplexer.ConnectionFailed += c.ConnectionFailed;
			multiplexer.ConnectionRestored += c.ConnectionRestored;
			return multiplexer;
		}

		/// <summary>
		/// 获取连接实例
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static IConnectionMultiplexer GetConnectionMultiplexer(Models.RedisConfig config = null)
		{
			var c = config ?? Config;
			var configString = CreateConfiguration(c);
			if (!ConnectionMultiplexers.TryGetValue(configString, out var multiplexer) || !multiplexer.IsConnected)
			{
				lock (Locker)
				{
					multiplexer = CreateConnectionMultiplexer(c);
					ConnectionMultiplexers[configString] = multiplexer;
				}
			}
			return multiplexer;
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static IDatabase GetDatabase(int db = -1, object asyncState = null, Models.RedisConfig config = null)
		{
			var multiplexer = GetConnectionMultiplexer(config);
			return multiplexer.GetDatabase(db, asyncState);
		}
	}
}
