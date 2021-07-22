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
		/// configuration
		/// </summary>
		static RedisConfiguration configuration;

		/// <summary>
		/// Configuration
		/// </summary>
		public static RedisConfiguration Configuration
		{
			get
			{
				if (configuration == null)
				{
					var config = JsonConfigHelper.Get("Redis");
					if (config != null)
					{
						configuration = new RedisConfiguration()
						{
							Host = config.Value<string>("Host"),
							Port = config.Value<int>("Port"),
							Password = config.Value<string>("Password"),
							DbNumber = config.Value<int>("DbNumber")
						};
					}
					else
					{
						throw new Exception("Not configured.");
					}
				}
				return configuration;
			}
		}

		/// <summary>
		/// DbCount
		/// </summary>
		public static readonly int DbCount = 16;
		/// <summary>
		/// Instance
		/// </summary>
		public static ConnectionMultiplexer Instance => CreateInstance(Configuration);

		/// <summary>
		/// Db
		/// </summary>
		public static IDatabase Db = Instance.GetDatabase(Configuration.DbNumber);
		/// <summary>
		/// Db0
		/// </summary>
		public static IDatabase Db0 = Instance.GetDatabase(0);
		/// <summary>
		/// Db1
		/// </summary>
		public static IDatabase Db1 = Instance.GetDatabase(1);
		/// <summary>
		/// Db2
		/// </summary>
		public static IDatabase Db2 = Instance.GetDatabase(2);
		/// <summary>
		/// Db3
		/// </summary>
		public static IDatabase Db3 = Instance.GetDatabase(3);
		/// <summary>
		/// Db4
		/// </summary>
		public static IDatabase Db4 = Instance.GetDatabase(4);
		/// <summary>
		/// Db15
		/// </summary>
		public static IDatabase Db15 = Instance.GetDatabase(15);

		/// <summary>
		/// CreateConnection
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static ConnectionMultiplexer CreateInstance(RedisConfiguration configuration = null)
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
		public static IDatabase CreateDatabase(RedisConfiguration configuration = null)
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
