using AhDai.Core;
using AhDai.Service;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 接口
/// </summary>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/interface")]
public class InterfaceController : ControllerBase
{
	readonly IInterfaceService _service;

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="service"></param>
	public InterfaceController(ILogger<InterfaceController> logger, IInterfaceService service)
	{
		_service = service;
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpGet("get")]
	public async Task<ApiResult<ICollection<InterfaceOutput>>> GetAsync([FromQuery] InterfaceQueryInput input)
	{
		var list = await _service.GetAsync(input);
		return ApiResult.Success(list);
	}
}