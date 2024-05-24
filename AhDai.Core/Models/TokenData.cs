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
    public string Id { get; set; } = default!;
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = default!;
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; } = default!;
    /// <summary>
    /// 平台
    /// </summary>
    public string Platform { get; set; } = default!;
    /// <summary>
    /// 扩展数据：存储键为 Ext-{key}
    /// </summary>
    public IDictionary<string, string> Extensions { get; set; } = default!;

}
