using AhDai.Integration.Attributes;

namespace AhDai.Integration.Hikvision.Configs;

/// <summary>
/// HikIoTConfig
/// </summary>
public class HikIoTConfig : BaseHikvisionConfig
{
    /// <summary>
    /// 用户名：登录账号手机号
    /// </summary>
    public string Username { get; set; } = default!;
    /// <summary>
    /// 密码
    /// </summary>
    [Sensitive]
    public string Password { get; set; } = default!;
}
