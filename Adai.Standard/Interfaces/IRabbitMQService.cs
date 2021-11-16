using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Adai.Standard.Interfaces
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

		/// <summary>
		/// 订阅队列
		/// </summary>
		/// <param name="queue">队列</param>
		/// <param name="recived">接收消息处理方法</param>
		/// <param name="autoStart">自动启动</param>
		/// <returns></returns>
		public string Subscribe(string queue, Func<object, BasicDeliverEventArgs, RabbitMQ.ResultType> recived, bool autoStart = true);

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="basicProperties">属性</param>
		/// <param name="body">内容</param>
		public void Publish(string exchange, string routingKey, IBasicProperties basicProperties, ReadOnlyMemory<byte> body);

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="basicProperties">属性</param>
		/// <param name="body">内容</param>
		public void Publish(string exchange, string routingKey, IBasicProperties basicProperties, string body);
	}
}
