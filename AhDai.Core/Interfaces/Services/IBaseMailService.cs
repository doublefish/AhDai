using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Interfaces.Services;

/// <summary>
/// IMailService
/// </summary>
public interface IBaseMailService
{

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="recipient">接收人</param>
    /// <param name="subject">主题</param>
    /// <param name="body">正文</param>
    /// <param name="options">自定义配置</param>
    /// <param name="cancellationToken"></param>
    Task SendAsync(string recipient, string subject, string body, Options.MailOptions? options = null, CancellationToken cancellationToken = default)
        => SendAsync([recipient], subject, body, options, cancellationToken);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="recipients">接收人</param>
    /// <param name="subject">主题</param>
    /// <param name="body">正文</param>
    /// <param name="options">自定义配置</param>
    /// <param name="cancellationToken"></param>
    Task SendAsync(IEnumerable<string> recipients, string subject, string body, Options.MailOptions? options = null, CancellationToken cancellationToken = default);
}
