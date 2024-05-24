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
    public string Id { get; set; } = default!;
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = default!;
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; } = default!;
    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime Expiration { get; set; }
    /// <summary>
    /// 认证类型：Bearer
    /// </summary>
    public string Type { get; set; } = default!;
}
