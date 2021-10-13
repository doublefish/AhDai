using RabbitMQ.Client;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// Helper
	/// </summary>
	public static class Helper
	{
		/// <summary>
		/// Config
		/// </summary>
		public static Config Config { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="config"></param>
		public static void Init(Config config)
		{
			Config = config;
		}

		/// <summary>
		/// CreateConnectionFactory
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IAsyncConnectionFactory CreateConnectionFactory(Config config = null)
		{
			if (config == null)
			{
				config = Config;
			}
			return new ConnectionFactory
			{
				HostName = config.Host,
				VirtualHost = config.VirtualHost,
				Port = config.Port,
				UserName = config.Username,
				Password = config.Password
			};
		}
	}
}
