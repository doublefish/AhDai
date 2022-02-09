using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Adai.Core.RabbitMQ
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
		/// <param name="model">amqp</param>
		/// <param name="exchange">交换器</param>
		public static void ExchangeDeclare(this IModel model, Exchange exchange)
		{
			model.ExchangeDeclare(exchange.Name, exchange.Type.GetString(), exchange.Durable, exchange.AutoDelete, exchange.Arguments);
		}

		/// <summary>
		/// 声明队列
		/// </summary>
		/// <param name="model">amqp</param>
		/// <param name="queue">队列</param>
		public static QueueDeclareOk QueueDeclare(this IModel model, Queue queue)
		{
			return model.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
		}

		/// <summary>
		/// 绑定队列
		/// </summary>
		/// <param name="model">amqp</param>
		/// <param name="queue">队列</param>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		public static void QueueBind(this IModel model, Queue queue, string exchange, string routingKey)
		{
			model.QueueBind(queue.Name, exchange, routingKey);
		}

		/// <summary>
		/// 声明队列并绑定到已存在的交换器
		/// </summary>
		/// <param name="model">amqp</param>
		/// <param name="queue">队列</param>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		public static void QueueDeclareAndBind(this IModel model, Queue queue, string exchange, string routingKey)
		{
			model.QueueDeclare(queue);
			model.QueueBind(queue.Name, exchange, routingKey);
		}

		/// <summary>
		/// 声明交换器和队列并绑定
		/// </summary>
		/// <param name="model">amqp</param>
		/// <param name="exchange">交换器</param>
		/// <param name="queue">队列</param>
		/// <param name="routingKey">路由</param>
		public static void DeclareAndBind(this IModel model, Exchange exchange, Queue queue, string routingKey)
		{
			model.ExchangeDeclare(exchange);
			model.QueueDeclare(queue);
			model.QueueBind(queue.Name, exchange.Name, routingKey);
		}

		/// <summary>
		/// 确认消息
		/// </summary>
		/// <param name="model">amqp</param>
		/// <param name="deliveryTag">交货标签</param>
		/// <param name="result">结果</param>
		public static void Ack(this IModel model, ulong deliveryTag, ResultType result)
		{
			switch (result)
			{
				case ResultType.Success:
					// 业务处理成功，从队列中移除
					model.BasicAck(deliveryTag, false);
					break;
				case ResultType.Fail:
					// 从队列中移除
					model.BasicNack(deliveryTag, false, false);
					break;
				case ResultType.Retry:
					// 添加到队列尾部
					model.BasicNack(deliveryTag, false, true);
					break;
				case ResultType.Exception:
					// 从队列中移除
					model.BasicNack(deliveryTag, false, false);
					break;
				default:
					goto case ResultType.Fail;
			}
		}

		/// <summary>
		/// 订阅队列
		/// </summary>
		/// <param name="model">amqp</param>
		/// <param name="queue">队列</param>
		/// <param name="received">接收消息处理方法</param>
		/// <returns></returns>
		public static string BasicConsume(this IModel model, string queue, Func<object, BasicDeliverEventArgs, ResultType> received)
		{
			// 消费者
			var consumer = new EventingBasicConsumer(model);
			consumer.Received += (sender, eventArgs) =>
			{
				ResultType result;
				try
				{
					result = received.Invoke(sender, eventArgs);
				}
				catch
				{
					result = ResultType.Exception;
				}
				model.Ack(eventArgs.DeliveryTag, result);
			};
			return model.BasicConsume(queue, false, consumer);
		}

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="model">amqp</param>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="basicProperties">属性</param>
		/// <param name="body">内容</param>
		public static void BasicPublish(this IModel model, string exchange, string routingKey, IBasicProperties basicProperties, string body)
		{
			model.BasicPublish(exchange, routingKey, basicProperties, Encoding.UTF8.GetBytes(body));
		}
	}
}
