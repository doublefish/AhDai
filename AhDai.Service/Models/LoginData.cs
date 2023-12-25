using AhDai.Core.Models;
using System.Collections.Generic;

namespace AhDai.Service.Models;

/// <summary>
/// 登录数据
/// </summary>
public class LoginData : TokenData
{
	string _companyCode;

	/// <summary>
	/// 公司编码
	/// </summary>
	public string CompanyCode
	{
		get
		{
			_companyCode ??= Extensions.TryGetValue("CompanyCode", out var value) ? value : string.Empty;
			return _companyCode;
		}
		set
		{
			_companyCode = value;
			Extensions ??= new Dictionary<string, string>();
			Extensions["CompanyCode"] = value;
		}
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	public LoginData()
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="data"></param>
	public LoginData(TokenData data) : this()
	{
		Id = data.Id;
		Username = data.Username;
		Name = data.Name;
		Type = data.Type;
		Platform = data.Platform;
		Extensions = data.Extensions;
	}
}
