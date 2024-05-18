using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Models;

/// <summary>
/// 修改头像
/// </summary>
public class AccountModifyAvatarInput : BaseInput
{
    /// <summary>
    /// 头像Id
    /// </summary>
    [Required]
    public long AvatarId { get; set; }

}
