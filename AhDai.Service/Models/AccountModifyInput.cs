using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AhDai.Service.Models;

/// <summary>
/// 修改信息
/// </summary>
public class AccountModifyInput : BaseInput
{
    /// <summary>
    /// 头像
    /// </summary>
    [JsonIgnore]
    public long? AvatarId { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string? Nickname { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    [Required]
    public string Name { get; set; } = "";
    /// <summary>
    /// 生日
    /// </summary>
    public DateTime? Birthday { get; set; }
    /// <summary>
    /// 性别：1-男，2-女
    /// </summary>
    [Required]
    public Shared.Enums.Gender Gender { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 手机
    /// </summary>
    [Required]
    public string MobilePhone { get; set; } = "";
    /// <summary>
    /// 电话
    /// </summary>
    public string? Telephone { get; set; }
}
