using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Models;

/// <summary>
/// 登录
/// </summary>
public class LoginInput
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public required string Username { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public required string Password { get; set; }
    ///// <summary>
    ///// 验证码标识
    ///// </summary>
    //[Required]
    //public string CaptchaId { get; set; }
    ///// <summary>
    ///// 验证码
    ///// </summary>
    //[Required]
    //public string Captcha { get; set; }
}
