namespace AhDai.Core.Configs;

/// <summary>
/// MailConfig
/// </summary>
public class MailConfig
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
    /// Smtp用户名
    /// </summary>
    public string SmtpUsername { get; set; } = null!;
    /// <summary>
    /// Smtp密码
    /// </summary>
    public string SmtpPassword { get; set; } = null!;
}
