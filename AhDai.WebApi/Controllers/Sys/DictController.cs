using AhDai.Core;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers.Sys;

/// <summary>
/// �ֵ�
/// </summary>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/dict")]
public class DictController(IDictService service) 
	: BaseCrudController<IDictService, DictInput, DictOutput, DictQueryInput>(service)
{
	/// <summary>
	/// �Ƿ����
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpGet("exist/{id}")]
	public async Task<IApiResult<bool>> ExistAsync([FromRoute] long id, [FromQuery] DicitCodeExistInput input)
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