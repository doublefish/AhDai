using AhDai.Core;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers.Sys;

/// <summary>
/// 角色
/// </summary>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/role")]
public class RoleController(IRoleService service)
	: BaseCrudController<IRoleService, RoleInput, RoleOutput, RoleQueryInput>(service)
{
	/// <summary>
	/// 是否存在
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpGet("exist/{id}")]
	public async Task<IApiResult<bool>> ExistAsync([FromRoute] long id, [FromQuery] CodeExistInput input)
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
	/// 查询已有菜单
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("menu/{id}")]
	public async Task<IApiResult<long[]>> GetMenuAsync([FromRoute] long id)
	{
		var res = await _service.GetMenuIdAsync(id);
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 分配菜单
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpPut("menu/{id}")]
	public async Task<IApiResult<string>> SaveMenuAsync([FromRoute] long id, [FromBody] RoleMenuInput input)
	{
		await _service.SaveMenuAsync(id, input);
		return ApiResult.Success();
	}
}