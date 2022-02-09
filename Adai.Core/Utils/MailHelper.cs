using System.Net;
using System.Net.Mail;

namespace Adai.Core.Utils
{
	/// <summary>
	/// MailHelper
	/// </summary>
	public static class MailHelper
	{
		/// <summary>
		/// 配置
		/// </summary>
		public static Models.MailConfig Config { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="config">配置</param>
		public static void Init(Models.MailConfig config)
		{
			Config = config;
		}

		/// <summary>
		/// 创建客户端
		/// </summary>
		/// <param name="config">自定义配置</param>
		/// <returns></returns>
		public static SmtpClient CreateSmtpClient(Models.MailConfig config = null)
		{
			var c = config ?? Config;
			return new SmtpClient()
			{
				Host = c.SmtpHost,
				Port = c.SmtpPort,
				EnableSsl = true,
				//UseDefaultCredentials = true,
				Credentials = new NetworkCredential(c.SmtpUsername, c.SmtpPassword)
			};
		}

		/// <summary>
		/// 发送
		/// </summary>
		/// <param name="recipients">接收人</param>
		/// <param name="subject">标题</param>
		/// <param name="body">内容</param>
		/// <param name="config">自定义配置</param>
		public static void Send(string recipients, string subject, string body, Models.MailConfig config = null)
		{
			var c = config ?? Config;
			using (var client = CreateSmtpClient(c))
			{
				client.Send(new MailMessage(c.SmtpUsername, recipients)
				{
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				});
				client.Dispose();
			};
		}
	}
}
