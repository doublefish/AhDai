using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// Base
	/// </summary>
	public class Base : IDisposable
	{
		/// <summary>
		/// Logger
		/// </summary>
		public ILogger Logger { get; private set; }
		/// <summary>
		/// 队列
		/// </summary>
		public string Queue { get; set; }
		/// <summary>
		/// 通道
		/// </summary>
		public string Exchange { get; set; }
		/// <summary>
		/// 路由
		/// </summary>
		public string RoutingKey { get; set; }

		/// <summary>
		/// Connection
		/// </summary>
		protected IConnection Connection { get; private set; }
		/// <summary>
		/// Channel
		/// </summary>
		protected IModel Channel { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="logger"></param>
		public Base(ILogger logger)
		{
			Logger = logger;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		protected void Init()
		{
			var factory = Helper.CreateConnectionFactory();
			Connection = factory.CreateConnection();
			Channel = Connection.CreateModel();
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			Channel?.Dispose();
			Connection?.Dispose();
		}
	}
}
