using AhDai.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

/// <summary>
/// 文件
/// </summary>
[ApiExplorerSettings(GroupName = SwaggerConfig.System)]
[Authorize]
[Route("api/file")]
public class FileController : ControllerBase
{
	//readonly IFileService _service;

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="service"></param>
	public FileController(ILogger<FileController> logger) 
	{
		//_service = service;
	}

	/// <summary>
	/// 上传
	/// </summary>
	/// <param name="files">文件，文件的name属性必须是files</param>
	/// <returns></returns>
	[HttpPost("upload")]
	public async Task<ApiResult<ICollection<FileOutput>>> UploadAsync([FromForm] IFormFile[] files)
	{
		//var res = await _service.UploadAsync(files);
		//return ApiResult.Success(null);
		return null;
	}
}