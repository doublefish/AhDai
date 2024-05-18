namespace AhDai.Service.Models;

/// <summary>
/// 验证码
/// </summary>
public class CaptchaOutput
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; } = "";
    /// <summary>
    /// 图片
    /// </summary>
    public string Image { get; set; } = "";
}
