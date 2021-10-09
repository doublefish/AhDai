﻿using System.Net;
using System.Net.Mail;

namespace Adai.Standard.Utilities
{
	/// <summary>
	/// MailHelper
	/// </summary>
	public static class MailHelper
	{
		/// <summary>
		/// Configuration
		/// </summary>
		public static Model.MailConfiguration Configuration { get; private set; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="configuration"></param>
		public static void Init(Model.MailConfiguration configuration)
		{
			Configuration = configuration;
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