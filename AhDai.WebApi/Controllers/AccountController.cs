using AhDai.Core;
using AhDai.Service;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 账号
/// </summary>
/// <param name="service"></param>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.Auth)]
[Route("api/v1/account")]
public class AccountController(IAccountService service) : ControllerBase
{
	readonly IAccountService _service = service;

	/// <summary>
	/// 账号信息
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IApiResult<AccountOutput>> GetAsync()
	{
		var res = await _service.GetAsync();
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 账号权限
	/// </summary>
	/// <returns></returns>
	[HttpGet("permission")]
	public async Task<IApiResult<AccountPermissionOutput>> GetPermissionAsync()
	{
		var res = await _service.GetPermissionAsync();
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 修改信息
	/// </summary>
	/// <returns></returns>
	[HttpPut]
	public async Task<IApiResult<string>> ModifyAsync([FromBody] AccountModifyInput input)
	{
		await _service.ModifyAsync(input);
		return ApiResult.Success();
	}

	/// <summary>
	/// 修改头像
	/// </summary>
	/// <returns></returns>
	[HttpPut("avatar")]
	public async Task<IApiResult<string>> ModifyAvatarAsync([FromBody] AccountModifyAvatarInput input)
	{
		await _service.ModifyAvatarAsync(input);
		return ApiResult.Success();
	}

	/// <summary>
	/// 修改密码
	/// </summary>
	/// <returns></returns>
	[HttpPut("password")]
	public async Task<IApiResult<string>> ChangePasswordAsync([FromBody] ChangePasswordInput input)
	{
		await _service.ChangePasswordAsync(input);
		return ApiResult.Success();
	}
}
