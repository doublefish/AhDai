using AhDai.Core;
using AhDai.Service;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 认证
/// </summary>
/// <param name="service"></param>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.Auth)]
[Route("api/auth")]
public class AuthController(IAuthService service) : ControllerBase
{
	readonly IAuthService _service = service;

	/// <summary>
	/// 获取验证码
	/// </summary>
	/// <returns></returns>
	[HttpGet("captcha")]
	public async Task<ApiResult<CaptchaOutput>> CaptchaAsync()
	{
		var res = await _service.GenerateCaptchaAsync();
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 获取登录信息
	/// </summary>
	/// <returns></returns>
	[Authorize]
	[HttpGet("login")]
	public ApiResult<LoginResult> GetLogin()
	{
		var token = HttpContext.Request.Headers[Const.Authorization].FirstOrDefault();
		var res = _service.GetLoginAsync(token);
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 登录
	/// </summary>
	/// <param name="input">入参</param>
	/// <returns></returns>
	[HttpPost("login")]
	public async Task<ApiResult<LoginResult>> LoginAsync([FromBody] LoginInput input)
	{
		var token = await _service.LoginAsync(input);
		return ApiResult.Success(token);
	}

	/// <summary>
	/// 登出
	/// </summary>
	/// <returns></returns>
	[HttpDelete("logout")]
	public async Task<ApiResult<bool>> LogoutAsync()
	{
		var token = HttpContext.Request.Headers[Const.Authorization].FirstOrDefault();
		var res = await _service.LogoutAsync(token);
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 刷新Token
	/// </summary>
	/// <returns></returns>
	[Authorize]
	[HttpPut("refreshToken")]
	public async Task<ApiResult<LoginResult>> RefreshTokenAsync()
	{
		var token = HttpContext.Request.Headers[Const.Authorization].FirstOrDefault();
		var res = await _service.RefreshTokenAsync(token);
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 注册
	/// </summary>
	/// <returns></returns>
	[Authorize]
	[HttpPost("signup")]
	public async Task<ApiResult<string>> SignupAsync([FromBody] SignupInput input)
	{
		await _service.SignupAsync(input);
		return ApiResult.Success();
	}
}
