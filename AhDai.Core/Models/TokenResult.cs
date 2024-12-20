using System;

namespace AhDai.Core.Models;

/// <summary>
/// TokenResult
/// </summary>
public class TokenResult
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string Id { get; set; } = null!;
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = null!;
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; } = null!;
    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime Expiration { get; set; }
    /// <summary>
    /// 认证类型：Bearer
    /// </summary>
    public string Type { get; set; } = null!;
}
