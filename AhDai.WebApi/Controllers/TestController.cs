using AhDai.Service;
using AhDai.Service.Test;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 项目
/// </summary>
/// <param name="service"></param>
/// <param name="dictService"></param>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/test")]
public class TestController(ITestService service, IDictTestService dictService) : ControllerBase
{
	readonly ITestService _service = service;
	readonly IDictTestService _dictService = dictService;

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

	/// <summary>
	/// 添加字典数据
	/// </summary>
	/// <returns></returns>
	[HttpPost("dict")]
	public async Task<object> AddDcitDataAsync()
	{
		var result = await _dictService.AddAsync();
		return result;
	}



	

}
