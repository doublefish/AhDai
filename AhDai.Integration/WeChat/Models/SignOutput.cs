namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 签名
/// </summary>
public class SignOutput
{
    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// 随机字符串
    /// </summary>
    public string NonceStr { get; set; } = default!;
    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
    /// <summary>
    /// 签名
    /// </summary>
    public string Signature { get; set; } = default!;
}
