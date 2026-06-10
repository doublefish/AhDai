using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.WeChat.Configs;

/// <summary>
/// BaseWeChatConfig
/// </summary>
public abstract class BaseWeChatConfig : BaseConfig
{
    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// AppSecret
    /// </summary>
    [Sensitive]
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// Token
    /// </summary>
    [Sensitive]
    public string Token { get; set; } = default!;
    /// <summary>
    /// AESKey
    /// </summary>
    [Sensitive]
    public string AESKey { get; set; } = default!;
}
