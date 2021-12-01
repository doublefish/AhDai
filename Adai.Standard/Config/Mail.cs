using System.Configuration;

namespace Adai.Standard.Config
{
	/// <summary>
	/// 邮件相关
	/// </summary>
	public static class Mail
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		static Mail()
		{
			var customize = ConfigurationManager.AppSettings["Mail.Customize"];
			if (customize == "1")
			{
				SmtpHost = ConfigurationManager.AppSettings["Mail.SmtpHost"];
				SmtpPort = ConfigurationManager.AppSettings["Mail.SmtpPort"];
				SmtpUsername = ConfigurationManager.AppSettings["Mail.SmtpUsername"];
				SmtpPassword = ConfigurationManager.AppSettings["Mail.SmtpPassword"];
			}
		}
		/// <summary>
		/// Smtp主机 默认：mail:smtp:host
		/// </summary>
		public static readonly string SmtpHost = "mail:smtp:host";
		/// <summary>
		/// Smtp端口 默认：mail:smtp:port
		/// </summary>
		public static readonly string SmtpPort = "mail:smtp:port";
		/// <summary>
		/// Smtp用户名 默认：mail:smtp:username
		/// </summary>
		public static readonly string SmtpUsername = "mail:smtp:username";
		/// <summary>
		/// Smtp密码 默认：mail:smtp:password
		/// </summary>
		public static readonly string SmtpPassword = "mail:smtp:password";
	}
}
