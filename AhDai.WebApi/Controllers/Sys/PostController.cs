using AhDai.Core;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers.Sys;

/// <summary>
/// 岗位
/// </summary>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/post")]
public class PostController(IPostService service) 
	: BaseCrudController<IPostService, PostInput, PostOutput, PostQueryInput>(service)
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
}