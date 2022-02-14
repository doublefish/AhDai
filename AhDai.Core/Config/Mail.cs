using System.Configuration;

namespace AhDai.Core.Config
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
		/// Smtp主机 默认：Mail:SmtpHost
		/// </summary>
		public static readonly string SmtpHost = "Mail:SmtpHost";
		/// <summary>
		/// Smtp端口 默认：Mail:SmtpPort
		/// </summary>
		public static readonly string SmtpPort = "Mail:SmtpPort";
		/// <summary>
		/// Smtp用户名 默认：Mail:SmtpUsername
		/// </summary>
		public static readonly string SmtpUsername = "Mail:SmtpUsername";
		/// <summary>
		/// Smtp密码 默认：Mail:SmtpPassword
		/// </summary>
		public static readonly string SmtpPassword = "Mail:SmtpPassword";
	}
}
