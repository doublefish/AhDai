using AhDai.Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// 邮件服务
/// </summary>
/// <param name="options"></param>
public class BaseMailService(IOptionsMonitor<Options.MailOptions> options) : IBaseMailService
{
    readonly IOptionsMonitor<Options.MailOptions> _options = options;

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="recipients">接收人</param>
    /// <param name="subject">主题</param>
    /// <param name="body">正文</param>
    /// <param name="options">自定义配置</param>
    /// <param name="cancellationToken">自定义配置</param>
    public virtual async Task SendAsync(IEnumerable<string> recipients, string subject, string body, Options.MailOptions? options = null, CancellationToken cancellationToken = default)
    {
        var o = options ?? _options.CurrentValue;
        using var client = CreateSmtpClient(o);
        using var message = new MailMessage()
        {
            From = string.IsNullOrWhiteSpace(o.FromDisplayName) ? new MailAddress(o.FromAddress) : new MailAddress(o.FromAddress, o.FromDisplayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        foreach (var recipient in recipients)
        {
            message.To.Add(recipient);
        }
        await client.SendMailAsync(message, cancellationToken);
    }

    /// <summary>
    /// 创建客户端
    /// </summary>
    /// <param name="options">自定义配置</param>
    /// <returns></returns>
    protected virtual SmtpClient CreateSmtpClient(Options.MailOptions? options = null)
    {
        var o = options ?? _options.CurrentValue;
        return new SmtpClient()
        {
            Host = o.SmtpHost,
            Port = o.SmtpPort,
            EnableSsl = o.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            //UseDefaultCredentials = true,
            Credentials = new NetworkCredential(o.SmtpUsername, o.SmtpPassword)
        };
    }
}
