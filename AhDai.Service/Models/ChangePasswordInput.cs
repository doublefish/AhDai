using System;
using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Models;

/// <summary>
/// 修改密码
/// </summary>
public class ChangePasswordInput
{
    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Password { get; set; } = "";
    /// <summary>
    /// 新密码
    /// </summary>
    [Required]
    public string NewPassword { get; set; } = "";
}
