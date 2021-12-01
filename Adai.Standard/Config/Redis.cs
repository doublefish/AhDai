using System.Configuration;

namespace Adai.Standard.Config
{
	/// <summary>
	/// Redis相关
	/// </summary>
	public static class Redis
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		static Redis()
		{
			var customize = ConfigurationManager.AppSettings["Redis.Customize"];
			if (customize == "1")
			{
				Host = ConfigurationManager.AppSettings["Redis.Host"];
				Port = ConfigurationManager.AppSettings["Redis.Port"];
				Password = ConfigurationManager.AppSettings["Redis.Password"];
			}
		}

		/// <summary>
		/// 主机 默认：Redis:Host
		/// </summary>
		public static readonly string Host = "Redis:Host";
		/// <summary>
		/// 端口 默认：Redis:Port
		/// </summary>
		public static readonly string Port = "Redis:Port";
		/// <summary>
		/// 密码 默认：Redis:Password
		/// </summary>
		public static readonly string Password = "Redis:Password";
	}
}
