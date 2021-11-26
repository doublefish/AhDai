using Adai.Standard.Extensions;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Adai.Standard.Services
{
	/// <summary>
	/// RabbitMQ服务
	/// </summary>
	public class RabbitMQService : Interfaces.IRabbitMQService
	{
		/// <summary>
		/// 配置
		/// </summary>
		public RabbitMQ.Config Config { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public RabbitMQService(IConfiguration configuration)
		{
			Config = configuration.GetRabbitMQConfig();
			RabbitMQ.Helper.Init(Config);
		}

		/// <summary>
		/// 获取连接
		/// </summary>
		/// <returns></returns>
		public IAsyncConnectionFactory GetConnectionFactory()
		{
			return RabbitMQ.Helper.GetConnectionFactory(Config);
		}

		/// <summary>
		/// 获取连接
		/// </summary>
		/// <returns></returns>
		public IConnection GetConnection()
		{
			var factory = GetConnectionFactory();
			return factory.CreateConnection();
		}
	}
}
