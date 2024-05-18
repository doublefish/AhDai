using AhDai.Core.Models;
using System;

namespace AhDai.Service.Models;

/// <summary>
/// 登录结果
/// </summary>
public class LoginResult
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

	/// <summary>
	/// 构造函数
	/// </summary>
	public LoginResult()
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="result"></param>
	public LoginResult(TokenResult result)
	{
		Token = result.Token;
		Expiration = result.Expiration;
		Type = result.Type;
	}
}
