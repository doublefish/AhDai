namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// RabbitMQConfig
	/// </summary>
	public class Config
	{
		/// <summary>
		/// 主机
		/// </summary>
		public string Host { get; set; }
		/// <summary>
		/// 端口
		/// </summary>
		public int Port { get; set; }
		/// <summary>
		/// 虚拟主机
		/// </summary>
		public string VirtualHost { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
	}
}
