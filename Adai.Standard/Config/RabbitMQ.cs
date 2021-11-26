using System.Configuration;

namespace Adai.Standard.Config
{
	/// <summary>
	/// RabbitMQ相关
	/// </summary>
	public static class RabbitMQ
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		static RabbitMQ()
		{
			var customize = ConfigurationManager.AppSettings["RabbitMQ.Customize"];
			if (customize == "1")
			{
				Host = ConfigurationManager.AppSettings["RabbitMQ.Host"];
				Port = ConfigurationManager.AppSettings["RabbitMQ.Port"];
				VirtualHost = ConfigurationManager.AppSettings["RabbitMQ.VirtualHost"];
				Username = ConfigurationManager.AppSettings["RabbitMQ.Username"];
				Password = ConfigurationManager.AppSettings["RabbitMQ.Password"];
			}
		}
		/// <summary>
		/// 主机
		/// </summary>
		public static readonly string Host = "rabbitmq:host";
		/// <summary>
		/// 端口
		/// </summary>
		public static readonly string Port = "rabbitmq:port";
		/// <summary>
		/// 虚拟主机
		/// </summary>
		public static readonly string VirtualHost = "rabbitmq:vhost";
		/// <summary>
		/// 用户名
		/// </summary>
		public static readonly string Username = "rabbitmq:username";
		/// <summary>
		/// 密码
		/// </summary>
		public static readonly string Password = "rabbitmq:password";
	}
}
