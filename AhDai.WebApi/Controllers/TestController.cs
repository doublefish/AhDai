using AhDai.Service;
using AhDai.Service.Test;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 项目
/// </summary>
/// <param name="service"></param>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/test")]
public class TestController(ITestService service) : ControllerBase
{
	readonly ITestService _service = service;

	/// <summary>
	/// 测试
	/// </summary>
	/// <returns></returns>
	[HttpGet("")]
	public async Task<object?> TestAsync()
	{
		var result = await _service.TestAsync();
		return result;
	}


}
