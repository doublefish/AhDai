using AhDai.Core;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers.Sys;

/// <summary>
/// �˵�
/// </summary>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/menu")]
public class MenuController(IMenuService service)
	: BaseCrudController<IMenuService, MenuInput, MenuOutput, MenuQueryInput>(service)
{
	/// <summary>
	/// ����
	/// </summary>
	/// <returns></returns>
	[HttpGet("config")]
	public async Task<IApiResult<MenuConfigOutput>> GetConfigAsync()
	{
		var res = await _service.GetConfigAsync();
		return ApiResult.Success(res);
	}

	/// <summary>
	/// ��ѯ���У����νṹ
	/// </summary>
	/// <returns></returns>
	[HttpGet("all")]
	public virtual async Task<IApiResult<MenuOutput[]>> GetAllAsync()
	{
		var res = await _service.GetAllAsync();
		return ApiResult.Success(res);
	}

	/// <summary>
	/// �Ƿ����
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
	/// ����
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
	/// ����
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