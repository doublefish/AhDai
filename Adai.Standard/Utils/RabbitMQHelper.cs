using RabbitMQ.Client;

namespace Adai.Standard.Utils
{
	/// <summary>
	/// RabbitMQHelper
	/// </summary>
	public static class RabbitMQHelper
	{
		/// <summary>
		/// Config
		/// </summary>
		public static Models.RabbitMqConfig Config { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="config"></param>
		public static void Init(Models.RabbitMqConfig config)
		{
			Config = config;
		}

		/// <summary>
		/// CreateConnectionFactory
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IAsyncConnectionFactory CreateConnectionFactory(Models.RabbitMqConfig configuration = null)
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
