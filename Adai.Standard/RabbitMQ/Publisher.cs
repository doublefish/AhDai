using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// 生产者
	/// </summary>
	public class Publisher : Base
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
		/// 发布消息
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="message"></param>
		public void Publish(IBasicProperties properties, string message)
		{
			var body = Encoding.UTF8.GetBytes(message);
			// 绑定
			Channel.BasicPublish(Exchange, RoutingKey, properties, body);
		}
	}
}
