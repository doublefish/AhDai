using RabbitMQ.Client;

namespace AhDai.Core.Interfaces
{
	/// <summary>
	/// RabbitMQ服务
	/// </summary>
	public interface IRabbitMQService
	{
		/// <summary>
		/// 获取连接
		/// </summary>
		/// <returns></returns>
		public IAsyncConnectionFactory GetConnectionFactory();

		/// <summary>
		/// 获取连接
		/// </summary>
		/// <returns></returns>
		public IConnection GetConnection();
	}
}
