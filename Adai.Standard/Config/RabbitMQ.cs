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
		/// 主机 默认：RabbitMQ:Host
		/// </summary>
		public static readonly string Host = "RabbitMQ:Host";
		/// <summary>
		/// 端口 默认：RabbitMQ:Port
		/// </summary>
		public static readonly string Port = "RabbitMQ:Port";
		/// <summary>
		/// 虚拟主机 默认：RabbitMQ:VirtualHost
		/// </summary>
		public static readonly string VirtualHost = "RabbitMQ:VirtualHost";
		/// <summary>
		/// 用户名 默认：RabbitMQ:Username
		/// </summary>
		public static readonly string Username = "RabbitMQ:Username";
		/// <summary>
		/// 密码 默认：RabbitMQ:Password
		/// </summary>
		public static readonly string Password = "RabbitMQ:Password";
	}
}
