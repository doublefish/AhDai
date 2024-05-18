using AhDai.Core;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers.Sys;

/// <summary>
/// 文件
/// </summary>
/// <param name="service"></param>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Route("api/v1/file")]
public class FileController(IFileService service) : ControllerBase
{
	readonly IFileService _service = service;

	/// <summary>
	/// 上传
	/// </summary>
	/// <param name="file">文件，文件的name属性必须是file</param>
	/// <returns></returns>
	[HttpPost("upload")]
	public async Task<IApiResult<FileOutput>> UploadAsync([FromForm] IFormFile file)
	{
		var res = await _service.UploadAsync(file);
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 批量上传
	/// </summary>
	/// <param name="files">文件，文件的name属性必须是files</param>
	/// <returns></returns>
	[HttpPost("uploads")]
	public async Task<IApiResult<FileOutput[]>> UploadAsync([FromForm] IFormFile[] files)
	{
		var res = await _service.UploadAsync(files);
		return ApiResult.Success(res);
	}

	/// <summary>
	/// 分页查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpGet("page")]
	public async Task<IApiResult<PageData<FileOutput>>> PageAsync([FromQuery] FileQueryInput input)
	{
		var res = await _service.PageAsync(input);
		return ApiResult.Success(res);
	}

}