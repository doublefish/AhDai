using System.Net.Mail;

namespace AhDai.Core.Services
{
	/// <summary>
	/// IMailService
	/// </summary>
	public interface IMailService
	{
		/// <summary>
		/// 创建客户端
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		SmtpClient CreateSmtpClient(Configs.MailConfig config = null);

		/// <summary>
		/// 发送
		/// </summary>
		/// <param name="recipients">接收人</param>
		/// <param name="subject">主题</param>
		/// <param name="body">正文</param>
		/// <param name="config">自定义配置</param>
		void Send(string recipients, string subject, string body, Configs.MailConfig config = null);
	}
}
