using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace AhDai.Core.Services;

/// <summary>
/// 邮件服务
/// </summary>
public class BaseMailService : IBaseMailService
{
    /// <summary>
    /// 配置
    /// </summary>
    public Configs.MailConfig Config { get; private set; }
    /// <summary>
    /// 日志
    /// </summary>
    public ILogger<BaseMailService> Logger { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public BaseMailService(IConfiguration configuration, ILogger<BaseMailService> logger)
    {
        Config = configuration.GetMailConfig();
        Logger = logger;
        if (Logger.IsEnabled(LogLevel.Debug)) Logger.LogDebug("Init=>Config={Config}", JsonUtil.Serialize(Config));
    }

    /// <summary>
    /// 创建客户端
    /// </summary>
    /// <param name="config">自定义配置</param>
    /// <returns></returns>
    public virtual SmtpClient CreateSmtpClient(Configs.MailConfig? config = null)
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
    public virtual void Send(string recipients, string subject, string body, Configs.MailConfig? config = null)
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
        }
        ;
    }
}
