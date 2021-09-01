using Adai.Standard.Model;
using System;
using System.Net;
using System.Net.Mail;

namespace Adai.Standard
{
	/// <summary>
	/// MailHelper
	/// </summary>
	public static class MailHelper
	{
		static Model.MailConfiguration _Configuration;

		/// <summary>
		/// Configuration
		/// </summary>
		public static Model.MailConfiguration Configuration
		{
			get
			{
				if (!Initialized)
				{
					throw new Exception("默认配置未初始化");
				}
				return _Configuration;
			}
		}

		/// <summary>
		/// 已初始化
		/// </summary>
		public static bool Initialized => _Configuration != null
			&& !string.IsNullOrEmpty(_Configuration.Host) && _Configuration.Port > 0
			&& !string.IsNullOrEmpty(_Configuration.Username)
			&& !string.IsNullOrEmpty(_Configuration.Password);

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static bool Init(Model.MailConfiguration configuration)
		{
			_Configuration = configuration;
			return Initialized;
		}

		/// <summary>
		/// CreateSmtpClient
		/// </summary>
		public static SmtpClient CreateSmtpClient()
		{
			return new SmtpClient()
			{
				Host = Configuration.Host,
				Port = Configuration.Port,
				EnableSsl = true,
				//UseDefaultCredentials = true,
				Credentials = new NetworkCredential(Configuration.Username, Configuration.Password)
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
				client.Send(new MailMessage(Configuration.Username, recipients)
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
