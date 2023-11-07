using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AhDai.Core.Services.Impl
{
    /// <summary>
    /// RabbitMQ发布服务
    /// </summary>
    public class PublishService : RabbitMQService, IPublishService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public PublishService(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="exchange">交换器</param>
        /// <param name="routingKey">路由</param>
        /// <param name="messageId">消息Id</param>
        /// <param name="headers">头</param>
        /// <param name="body">内容</param>
        public void Publish(string exchange, string routingKey, string messageId, IDictionary<string, object> headers, ReadOnlyMemory<byte> body)
        {
            var factory = GetConnectionFactory();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.MessageId = messageId;
            basicProperties.Headers = headers;
            channel.BasicPublish(exchange, routingKey, basicProperties, body);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="exchange">交换器</param>
        /// <param name="routingKey">路由</param>
        /// <param name="messageId">消息Id</param>
        /// <param name="headers">头</param>
        /// <param name="body">内容</param>
        public void Publish(string exchange, string routingKey, string messageId, IDictionary<string, object> headers, string body)
        {
            Publish(exchange, routingKey, messageId, headers, Encoding.UTF8.GetBytes(body));
        }
    }
}
