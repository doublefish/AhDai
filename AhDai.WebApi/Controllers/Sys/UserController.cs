using AhDai.Core;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers.Sys;

/// <summary>
/// 用户
/// </summary>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/user")]
public class UserController(IUserService service)
	: BaseCrudController<IUserService, UserInput, UserOutput, UserQueryInput>(service)
{
	/// <summary>
	/// 配置
	/// </summary>
	/// <returns></returns>
	[HttpGet("config")]
	public async Task<IApiResult<UserConfigOutput>> GetConfigAsync()
	{
		var res = await _service.GetConfigAsync();
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 是否存在
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpGet("exist/{id}")]
	public async Task<IApiResult<bool>> ExistAsync([FromRoute] long id, [FromQuery] UserExistInput input)
	{
		var res = await _service.ExistAsync(id, input);
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 启用
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpPut("enable/{id}")]
	public async Task<IApiResult<string>> EnableAsync([FromRoute] long id)
	{
		await _service.EnableAsync(id);
		return ApiResult.Success();
	}

	/// <summary>
	/// 禁用
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpPut("disable/{id}")]
	public async Task<IApiResult<string>> DisableAsync([FromRoute] long id)
	{
		await _service.DisableAsync(id);
		return ApiResult.Success();
	}

	/// <summary>
	/// 重置密码
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpPut("resetPassword/{id}")]
	public async Task<IApiResult<string>> ResetPasswordAsync([FromRoute] long id)
	{
		await _service.ResetPasswordAsync(id);
		return ApiResult.Success();
	}
}