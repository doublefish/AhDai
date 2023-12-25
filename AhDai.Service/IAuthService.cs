using AhDai.Service.Models;
using System;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IAuthService
/// </summary>
public interface IAuthService : IBaseService
{
	/// <summary>
	/// 生成验证码
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	Task<CaptchaOutput> GenerateCaptchaAsync();

	/// <summary>
	/// 登录
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	Task<LoginResult> LoginAsync(LoginInput input);

	/// <summary>
	/// 登出
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	Task<bool> LogoutAsync(string token);

	/// <summary>
	/// 获取登录信息
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	LoginResult GetLoginAsync(string token);

	/// <summary>
	/// 刷新Token
	/// </summary>
	/// <param name="token"></param>
	/// <returns></returns>
	Task<LoginResult> RefreshTokenAsync(string token);

	/// <summary>
	/// 移除Token
	/// </summary>
	/// <param name="token"></param>
	Task<bool> RemoveTokenAsync(string token);

	/// <summary>
	/// 注册
	/// </summary>
	/// <param name="input">入参</param>
	/// <returns></returns>
	Task SignupAsync(SignupInput input);
}
