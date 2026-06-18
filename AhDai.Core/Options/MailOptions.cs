namespace AhDai.Core.Options;

/// <summary>
/// MailOptions
/// </summary>
public class MailOptions
{
    /// <summary>
    /// Smtp主机
    /// </summary>
    public string SmtpHost { get; set; } = null!;
    /// <summary>
    /// Smtp端口
    /// </summary>
    public int SmtpPort { get; set; }
    /// <summary>
    /// EnableSsl
    /// </summary>
    public bool EnableSsl { get; set; }
    /// <summary>
    /// Smtp用户名
    /// </summary>
    public string SmtpUsername { get; set; } = null!;
    /// <summary>
    /// Smtp密码
    /// </summary>
    public string SmtpPassword { get; set; } = null!;
    /// <summary>
    /// 发送地址
    /// </summary>
    public string FromAddress { get; set; } = null!;
    /// <summary>
    /// 发送显示名
    /// </summary>
    public string? FromDisplayName { get; set; }
}
