namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 签名
/// </summary>
public class SignInput
{
    /// <summary>
    /// 当前网页的URL，不包含#及其后面部分
    /// </summary>
    public string Url { get; set; } = default!;
}
