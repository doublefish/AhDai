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
		/// <param name="configuration"></param>
		public static void Init(Models.MailConfig configuration)
		{
			Config = configuration;
		}

		/// <summary>
		/// CreateSmtpClient
		/// </summary>
		public static SmtpClient CreateSmtpClient()
		{
			return new SmtpClient()
			{
				Host = Config.SmtpHost,
				Port = Config.SmtpPort,
				EnableSsl = true,
				//UseDefaultCredentials = true,
				Credentials = new NetworkCredential(Config.SmtpUsername, Config.SmtpPassword)
			};
		}

		/// <summary>
		/// 发送
		/// </summary>
		/// <param name="recipients"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public static void Send(string recipients, string subject, string body)
		{
			using (var client = CreateSmtpClient())
			{
				client.Send(new MailMessage(Config.SmtpUsername, recipients)
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
