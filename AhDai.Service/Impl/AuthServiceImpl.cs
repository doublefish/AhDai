using AhDai.Core;
using AhDai.Core.Services;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// AuthServiceImpl
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="serviceProvider"></param>
internal class AuthServiceImpl(IServiceProvider serviceProvider) : IAuthService
{
	readonly IJwtService JwtService = serviceProvider.GetRequiredService<IJwtService>();
	readonly IDictService DictService = serviceProvider.GetRequiredService<IDictService>();

	/// <summary>
	/// 生成验证码
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public async Task<CaptchaOutput> GenerateCaptchaAsync()
	{
		await Task.Run(() =>
		{
			
		});
		//var result = await GenerateCaptchaAsync();
		return new CaptchaOutput()
		{
			Id = "",
			Image = ""
		};
	}

	/// <summary>
	/// 登录
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	/// <exception cref="ApiException"></exception>
	public async Task<LoginResult> LoginAsync(LoginInput input)
	{
		return await Login(input.Username);
	}

	/// <summary>
	/// 登出
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	public async Task<bool> LogoutAsync(string token)
	{
		await Task.Run(() =>
		{

		});
		return true;
	}

	/// <summary>
	/// 获取登录信息
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	public LoginResult GetLoginAsync(string token)
	{
		var data = JwtService.GetTokenData(token);
		var loginData = new LoginData(data);
		return new LoginResult()
		{
			Username = loginData.Username,
			CompanyCode = loginData.CompanyCode
		};
	}

	/// <summary>
	/// 刷新Token
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	public async Task<LoginResult> RefreshTokenAsync(string token)
	{
		var res = await JwtService.RefreshTokenAsync(token);
		return new LoginResult(res);
	}

	/// <summary>
	/// 移除Token
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	public async Task<bool> RemoveTokenAsync(string token)
	{
		return await JwtService.RemoveTokenAsync(token);
	}

	/// <summary>
	/// 注册
	/// </summary>
	/// <param name="input">入参</param>
	/// <returns></returns>
	public async Task SignupAsync(SignupInput input)
	{
		var tokenData = MyApp.GetCurrentTokenData();
		if (tokenData.Username != "test")
		{
			throw new Exception("无权访问此接口");
		}
		await Task.Run(() =>
		{

		});
	}


	/// <summary>
	/// 登录
	/// </summary>
	/// <param name="username">用户名</param>
	/// <returns></returns>
	/// <exception cref="ApiException"></exception>
	private async Task<LoginResult> Login(string username)
	{
		if (await IsInNameListAsync(username, MyConst.DictCode.LOGIN_BLACKLIST))
		{
			throw new ApiException(StatusCodes.Status403Forbidden, "无登录权限");
		}
		var allowLogin = await IsInNameListAsync(username, MyConst.DictCode.LOGIN_WHITELIST);
		if (!allowLogin)
		{
			//
		}
		if (!allowLogin)
		{
			throw new ApiException(StatusCodes.Status403Forbidden, "无登录权限");
		}

		var data = new LoginData()
		{
			Id = "",
			Username = "",
			Name = "",
			CompanyCode = ""
		};
		var res = await JwtService.GenerateTokenAsync(data);
		var result = new LoginResult(res)
		{
			CompanyCode = ""
		};
		return result;
	}

	/// <summary>
	/// 是否在名单里
	/// </summary>
	/// <param name="name">名字/工号</param>
	/// <param name="nameListCode">名单编码</param>
	/// <returns></returns>
	private async Task<bool> IsInNameListAsync(string name, string nameListCode)
	{
		var dict = await DictService.GetByCodeAsync(nameListCode);
		if (dict.Data != null)
		{
			foreach (var item in dict.Data)
			{
				if (!string.IsNullOrEmpty(item.Value) && item.Value.Split('/').Contains(name))
				{
					return true;
				}
			}
		}
		return false;
	}
}
