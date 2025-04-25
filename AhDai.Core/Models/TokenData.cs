using System;
using System.Collections.Generic;

namespace AhDai.Core.Models;

/// <summary>
/// TokenData
/// </summary>
public class TokenData
{
    /// <summary>
    /// 用户标识
    /// </summary>
    public string Id { get; set; } = null!;
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = null!;
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; } = null!;
    /// <summary>
    /// 平台
    /// </summary>
    public string Platform { get; set; } = null!;
    /// <summary>
    /// 扩展数据
    /// </summary>
    public IDictionary<string, ICollection<string>> Extensions { get; set; } = null!;

}
