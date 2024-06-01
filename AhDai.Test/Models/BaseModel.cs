using Nest;
using System;

namespace AhDai.Test.Models;

public class BaseModel
{
    /// <summary>
    /// 数码
    /// </summary>
    [Number]
    public long Id { get; set; }
    /// <summary>
    /// 版本号
    /// </summary>
    [Number]
    public int RowVersion { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [Date]
    public DateTime RowCreateDate { get; set; }
    /// <summary>
    /// 创建用户
    /// </summary>
    [Keyword]
    public string RowCreateUser { get; set; } = "";
    /// <summary>
    /// 创建用户名
    /// </summary>
    [Keyword]
    public string RowCreateUsername { get; set; } = "";
    /// <summary>
    /// 修改时间
    /// </summary>
    [Date]
    public DateTime RowUpdateDate { get; set; }
    /// <summary>
    /// 修改用户
    /// </summary>
    [Keyword]
    public string? RowUpdateUser { get; set; }
    /// <summary>
    /// 修改用户名
    /// </summary>
    [Keyword]
    public string? RowUpdateUsername { get; set; }
    /// <summary>
    /// 删除标识
    /// </summary>
    [Boolean]
    public bool RowDeleted { get; set; }
    /// <summary>
    /// 删除时间
    /// </summary>
    [Date]
    public DateTime RowDeletedDate { get; set; }
    /// <summary>
    /// 删除用户
    /// </summary>
    [Keyword]
    public string? RowDeletedUser { get; set; }
}
