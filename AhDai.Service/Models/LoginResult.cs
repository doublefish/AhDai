using AhDai.Core.Models;
using System.Collections.Generic;

namespace AhDai.Service.Models;

/// <summary>
/// 登录结果
/// </summary>
public class LoginResult : TokenResult
{
	/// <summary>
	/// 公司编码
	/// </summary>
	public string CompanyCode { get; set; }

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
		Id = result.Id;
		Username = result.Username;
		Token = result.Token;
		Expiration = result.Expiration;
		Type = result.Type;
	}
}
