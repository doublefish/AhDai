using System.Net;
using System.Net.Mail;

namespace Adai.Standard.Utils
{
	/// <summary>
	/// MailHelper
	/// </summary>
	public static class MailHelper
	{
		/// <summary>
		/// Config
		/// </summary>
		public static Models.MailConfig Config { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="config"></param>
		public static void Init(Models.MailConfig config)
		{
			Config = config;
		}

		/// <summary>
		/// 创建客户端
		/// </summary>
		/// <param name="config"></param>
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
		/// <param name="recipients"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="config"></param>
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
