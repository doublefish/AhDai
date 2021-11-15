﻿using StackExchange.Redis;
using System.Collections.Generic;

namespace Adai.Standard.Utils
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
		/// Config
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
		/// <param name="config"></param>
		public static void Init(Models.RedisConfig config)
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
		public static string CreateConfiguration(Models.RedisConfig config)
		{
			return CreateConfiguration(config.Host, config.Port, config.Password);
		}

		/// <summary>
		/// 获取连接实例
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IConnectionMultiplexer GetConnectionMultiplexer(Models.RedisConfig config = null)
		{
			var c = config ?? Config;
			var str = CreateConfiguration(c);
			lock (Locker)
			{
				if (!ConnectionMultiplexers.TryGetValue(str, out var multiplexer) || !multiplexer.IsConnected)
				{
					multiplexer = ConnectionMultiplexer.Connect(str);

					// 注册事件
					multiplexer.ConfigurationChangedBroadcast += c.ConfigurationChangedBroadcast;
					multiplexer.ConfigurationChanged += c.ConfigurationChanged;
					multiplexer.HashSlotMoved += c.HashSlotMoved;
					multiplexer.ErrorMessage += c.ErrorMessage;
					multiplexer.InternalError += c.InternalError;
					multiplexer.ConnectionFailed += c.ConnectionFailed;
					multiplexer.ConnectionRestored += c.ConnectionRestored;
					ConnectionMultiplexers.Add(str, multiplexer);
				}
				return multiplexer;
			}
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(int db = -1, object asyncState = null, Models.RedisConfig config = null)
		{
			var multiplexer = GetConnectionMultiplexer(config);
			if (db == -1)
			{
				var c = config ?? Config;
				db = c.Database;
			}
			return multiplexer.GetDatabase(db, asyncState);
		}

		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(Models.RedisConfig config = null)
		{
			return GetDatabase(-1, null, config);
		}
	}
}
