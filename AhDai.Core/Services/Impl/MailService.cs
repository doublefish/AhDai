using AhDai.Core.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace AhDai.Core.Services.Impl
{
    /// <summary>
    /// MailService
    /// </summary>
    public class MailService : IMailService
    {
        /// <summary>
        /// 配置
        /// </summary>
        public Configs.MailConfig Config { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置</param>
        public MailService(IConfiguration configuration)
        {
            Config = configuration.GetMailConfig();
        }

        /// <summary>
        /// 创建客户端
        /// </summary>
        /// <param name="config">自定义配置</param>
        /// <returns></returns>
        public SmtpClient CreateSmtpClient(Configs.MailConfig config = null)
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
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        /// <param name="config">自定义配置</param>
        public void Send(string recipients, string subject, string body, Configs.MailConfig config = null)
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
