using RabbitMQ.Client;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// RabbitMQExtensions
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// GetString
		/// </summary>
		/// <param name="exchangeType"></param>
		/// <returns></returns>
		public static string GetString(this ExchangeType exchangeType)
		{
			return exchangeType switch
			{
				ExchangeType.Fanout => "fanout",
				ExchangeType.Direct => "direct",
				ExchangeType.Topic => "topic",
				ExchangeType.Headers => "headers",
				ExchangeType.Delayed => "x-delayed-message",
				_ => "",
			};
		}

		/// <summary>
		/// 声明交换器
		/// </summary>
		/// <param name="model"></param>
		/// <param name="exchange"></param>
		public static void ExchangeDeclare(this IModel model, Exchange exchange)
		{
			model.ExchangeDeclare(exchange.Name, exchange.Type.GetString(), exchange.Durable, exchange.AutoDelete, exchange.Arguments);
		}

		/// <summary>
		/// 声明队列
		/// </summary>
		/// <param name="model"></param>
		/// <param name="queue"></param>
		public static QueueDeclareOk QueueDeclare(this IModel model, Queue queue)
		{
			return model.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
		}

		/// <summary>
		/// 声明队列并绑定到已存在的交换器
		/// </summary>
		/// <param name="model"></param>
		/// <param name="queue"></param>
		/// <param name="exchange"></param>
		/// <param name="routingKey"></param>
		public static void QueueDeclareAndBind(this IModel model, Queue queue, string exchange, string routingKey)
		{
			model.QueueDeclare(queue);
			model.QueueBind(queue.Name, exchange, routingKey);
		}

		/// <summary>
		/// 声明交换器和队列并绑定
		/// </summary>
		/// <param name="model"></param>
		/// <param name="exchange"></param>
		/// <param name="queue"></param>
		/// <param name="routingKey"></param>
		public static void DeclareAndBind(this IModel model, Exchange exchange, Queue queue, string routingKey)
		{
			model.ExchangeDeclare(exchange);
			model.QueueDeclare(queue);
			model.QueueBind(queue.Name, exchange.Name, routingKey);
		}
	}
}
