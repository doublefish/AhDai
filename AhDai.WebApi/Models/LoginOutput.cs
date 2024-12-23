using System;

namespace AhDai.WebApi.Models;

/// <summary>
/// LoginOutput
/// </summary>
public class LoginOutput
{
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; } = "";
    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime Expiration { get; set; }
    /// <summary>
    /// 认证类型：Bearer
    /// </summary>
    public string Type { get; set; } = "";
}
