using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace AhDai.Test;

public class Class1
{
	public static void Main()
	{
		// 设置连接参数
		var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };

		// 建立连接和通道
		using (var connection = factory.CreateConnection())
		using (var channel = connection.CreateModel())
		{
			// 声明 Exchange
			channel.ExchangeDeclare(exchange: "myexchange", ExchangeType.Direct);

			// 声明队列
			channel.QueueDeclare(queue: "myqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

			// 绑定 Exchange 和 Queue
			channel.QueueBind(queue: "myqueue", exchange: "myexchange", routingKey: "myroutingkey");

			// 发送消息
			for (int i = 0; i < 5; i++)
			{
				string message = i.ToString();
				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(exchange: "myexchange", routingKey: "myroutingkey", basicProperties: null, body: body);
			}

			// 接收消息
			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				Console.WriteLine($"第{message}次开始{DateTime.Now}");

				Thread.Sleep(3000);

				Console.WriteLine($"第{message}次结束{DateTime.Now}");

			};
			channel.BasicConsume(queue: "myqueue", autoAck: true, consumer: consumer);

			Console.ReadLine();
			//日志输出
			//第0次开始2023 / 6 / 28 11:29:46
			//第0次结束2023 / 6 / 28 11:29:49
			//第1次开始2023 / 6 / 28 11:29:49
			//第1次结束2023 / 6 / 28 11:29:52
			//第2次开始2023 / 6 / 28 11:29:52
			//第2次结束2023 / 6 / 28 11:29:55
			//第3次开始2023 / 6 / 28 11:29:55
			//第3次结束2023 / 6 / 28 11:29:58
			//第4次开始2023 / 6 / 28 11:29:58
			//第4次结束2023 / 6 / 28 11:30:01
		}
	}
}

