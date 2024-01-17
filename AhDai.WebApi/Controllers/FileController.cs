using AhDai.Core;
using AhDai.Service;
using AhDai.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// 文件
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="logger"></param>
/// <param name="service"></param>
[ApiExplorerSettings(GroupName = Configs.SwaggerConfig.System)]
[Authorize]
[Route("api/file")]
public class FileController(IFileService service) : ControllerBase
{
	readonly IFileService _service = service;

	/// <summary>
	/// 上传
	/// </summary>
	/// <param name="files">文件，文件的name属性必须是files</param>
	/// <returns></returns>
	[HttpPost("upload")]
	public async Task<ApiResult<ICollection<FileOutput>>> UploadAsync([FromForm] IFormFile[] files)
	{
		var res = await _service.UploadAsync(files);
		return ApiResult.Success(res);
	}
}