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
		/// <summary>
		/// smtpConfiguration
		/// </summary>
		static SmtpConfiguration smtpConfiguration;
		/// <summary>
		/// 有效时间
		/// </summary>
		static TimeSpan expiry;

		/// <summary>
		/// SmptConfiguration
		/// </summary>
		public static SmtpConfiguration SmptConfiguration
		{
			get
			{
				if (smtpConfiguration == null)
				{
					var config = JsonConfigHelper.Get("Mail.Smtp");
					if (config != null)
					{
						smtpConfiguration = new SmtpConfiguration()
						{
							Host = config.Value<string>("Host"),
							Port = config.Value<int>("Port"),
							Username = config.Value<string>("Username"),
							Password = config.Value<string>("Password")
						};
					}
					else
					{
						throw new Exception("Not configured.");
					}
				}
				return smtpConfiguration;
			}
		}

		/// <summary>
		/// 有效时间
		/// </summary>
		public static TimeSpan Expiry
		{
			get
			{
				if (expiry == null || expiry == TimeSpan.Zero)
				{
					var config = JsonConfigHelper.Get("Mail.Expiry");
					if (config != null)
					{
						expiry = config.ToObject<TimeSpan>();
					}
					else
					{
						throw new Exception("Not configured.");
					}
				}
				return expiry;
			}
		}

		/// <summary>
		/// Client
		/// </summary>
		public static SmtpClient Client => new SmtpClient()
		{
			Host = SmptConfiguration.Host,
			Port = SmptConfiguration.Port,
			EnableSsl = true,
			//UseDefaultCredentials = true,
			Credentials = new NetworkCredential(SmptConfiguration.Username, SmptConfiguration.Password)
		};

		/// <summary>
		/// 发送
		/// </summary>
		/// <param name="recipients"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public static void Send(string recipients, string subject, string body)
		{
			using (var client = Client)
			{
				client.Send(new MailMessage(SmptConfiguration.Username, recipients)
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
