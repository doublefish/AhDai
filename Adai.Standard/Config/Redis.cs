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
		/// 主机
		/// </summary>
		public static readonly string Host = "redis:host";
		/// <summary>
		/// 端口
		/// </summary>
		public static readonly string Port = "redis:port";
		/// <summary>
		/// 密码
		/// </summary>
		public static readonly string Password = "redis:password";
	}
}
