using System;
using System.Collections.Generic;

namespace Adai.Standard.Interfaces
{
	/// <summary>
	/// 发布服务
	/// </summary>
	public interface IPublishService : IRabbitMQService
	{
		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="messageId">消息Id</param>
		/// <param name="headers">头</param>
		/// <param name="body">内容</param>
		public void Publish(string exchange, string routingKey, string messageId, IDictionary<string, object> headers, ReadOnlyMemory<byte> body);

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="exchange">交换器</param>
		/// <param name="routingKey">路由</param>
		/// <param name="messageId">消息Id</param>
		/// <param name="headers">头</param>
		/// <param name="body">内容</param>
		public void Publish(string exchange, string routingKey, string messageId, IDictionary<string, object> headers, string body);
	}
}
