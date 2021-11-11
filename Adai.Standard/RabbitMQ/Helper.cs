using RabbitMQ.Client;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// RabbitMQHelper
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
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IAsyncConnectionFactory CreateConnectionFactory(Config configuration = null)
		{
			if (configuration == null)
			{
				configuration = Config;
			}
			return new ConnectionFactory
			{
				HostName = configuration.Host,
				VirtualHost = configuration.VirtualHost,
				Port = configuration.Port,
				UserName = configuration.Username,
				Password = configuration.Password
			};
		}
	}
}
