using System;
using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 员工
/// </summary>
public class EmployeeInput : BaseInput
{
    /// <summary>
    /// 工号
    /// </summary>
    [Required]
    public string Number { get; set; } = "";
    /// <summary>
    /// 用户名：不可修改
    /// </summary>
    [Required]
    public string Username { get; set; } = "";
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
    /// 性别
    /// </summary>
    [Required]
    public Shared.Enums.Gender Gender { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 手机号码：不可修改
    /// </summary>
    [Required]
    public string MobilePhone { get; set; } = "";
    /// <summary>
    /// 电话
    /// </summary>
    public string? Telephone { get; set; }
    /// <summary>
    /// 身份证号
    /// </summary>
    public string IdNumber { get; set; } = "";
    /// <summary>
    /// 组织Id
    /// </summary>
    [Required]
    public long OrgId { get; set; }
    /// <summary>
    /// 岗位Id
    /// </summary>
    public long[]? PostIds { get; set; }
}
