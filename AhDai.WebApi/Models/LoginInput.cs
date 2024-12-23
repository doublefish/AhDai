using System.ComponentModel.DataAnnotations;

namespace AhDai.WebApi.Models;

/// <summary>
/// LoginInput
/// </summary>
public class LoginInput
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string Username { get; set; } = null!;
    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}
