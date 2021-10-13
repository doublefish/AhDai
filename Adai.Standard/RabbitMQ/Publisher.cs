using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// 生产者
	/// </summary>
	public class Publisher : Basic
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="logger"></param>
		public Publisher(ILogger logger) : base(logger)
		{
			Init();
		}

		/// <summary>
		/// 订阅消息
		/// </summary>
		public void Publish(string message)
		{
			var body = Encoding.UTF8.GetBytes(message);
			// 绑定
			Channel.BasicPublish(Exchange, RoutingKey, null, body);
		}
	}
}
