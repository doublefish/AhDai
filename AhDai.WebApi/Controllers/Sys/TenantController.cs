using AhDai.Core;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers.Sys;

/// <summary>
/// �⻧
/// </summary>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/tenant")]
public class TenantController(ITenantService service) 
	: BaseCrudController<ITenantService, TenantInput, TenantOutput, TenantQueryInput>(service)
{
	/// <summary>
	/// ����
	/// </summary>
	/// <returns></returns>
	[HttpGet("config")]
	public async Task<IApiResult<TenantConfigOutput>> GetConfigAsync()
	{
		var res = await _service.GetConfigAsync();
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

	/// <summary>
	/// ��ѯ���в˵�
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
	/// ����˵�
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpPut("menu/{id}")]
	public async Task<IApiResult<string>> SaveMenuAsync([FromRoute] long id, [FromBody] TenantMenuInput input)
	{
		await _service.SaveMenuAsync(id, input);
		return ApiResult.Success();
	}
}