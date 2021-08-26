using Adai.Standard.Model;
using StackExchange.Redis;
using System;

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
		/// 初始化
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static bool Init(Model.RedisConfiguration configuration)
		{
			Configuration = configuration;
			return true;
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
			return CreateInstance().GetDatabase(db, asyncState);
		}

		/// <summary>
		/// CreateConnection
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static ConnectionMultiplexer CreateInstance(Model.RedisConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}
			var str = CreateConfiguration(configuration.Host, configuration.Port, configuration.Password);
			return ConnectionMultiplexer.Connect(str);
		}

		/// <summary>
		/// CreateDatabase
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IDatabase CreateDatabase(Model.RedisConfiguration configuration = null)
		{
			using var instance = CreateInstance(configuration);
			return instance.GetDatabase(configuration.DbNumber);
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
