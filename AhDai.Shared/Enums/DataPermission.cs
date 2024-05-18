using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Enums;

/// <summary>
/// 数据权限类型
/// </summary>
public enum DataPermission
{
    /// <summary>
    /// 本人
    /// </summary>
    [Display(Name = "本人")]
    Self = 1,
    /// <summary>
    /// 本部门
    /// </summary>
    [Display(Name = "本部门")]
    Department = 2,
    /// <summary>
    /// 本部门及下级
    /// </summary>
    [Display(Name = "本部门及下级")]
    DepartmentAndSubordinates = 3,
    /// <summary>
    /// 全部
    /// </summary>
    [Display(Name = "全部")]
    All = 99,
}
