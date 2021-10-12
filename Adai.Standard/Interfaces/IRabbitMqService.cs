using Microsoft.Extensions.Logging;
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
		/// <param name="logger"></param>
		/// <returns></returns>
		public RabbitMQ.Publisher GetPublisher(ILogger logger);

		/// <summary>
		/// GetConsumer
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="func"></param>
		/// <returns></returns>
		public RabbitMQ.Consumer GetConsumer(ILogger logger, Func<string, bool> func);
	}
}
