using RabbitMQ.Client.Events;
using System;

namespace Adai.Standard.Interfaces
{
	/// <summary>
	/// RabbitMQ服务
	/// </summary>
	public interface IRabbitMqService
	{
		/// <summary>
		/// GetPublisher
		/// </summary>
		/// <returns></returns>
		public RabbitMQ.Publisher GetPublisher();

		/// <summary>
		/// GetConsumer
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public RabbitMQ.Consumer GetConsumer(Action<object, BasicDeliverEventArgs> action);
	}
}
