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
		/// 主机 默认：rabbitmq:host
		/// </summary>
		public static readonly string Host = "rabbitmq:host";
		/// <summary>
		/// 端口 默认：rabbitmq:port
		/// </summary>
		public static readonly string Port = "rabbitmq:port";
		/// <summary>
		/// 虚拟主机 默认：rabbitmq:virtualhost
		/// </summary>
		public static readonly string VirtualHost = "rabbitmq:virtualhost";
		/// <summary>
		/// 用户名 默认：rabbitmq:username
		/// </summary>
		public static readonly string Username = "rabbitmq:username";
		/// <summary>
		/// 密码 默认：rabbitmq:password
		/// </summary>
		public static readonly string Password = "rabbitmq:password";
	}
}
