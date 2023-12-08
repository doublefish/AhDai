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
/// �ļ�
/// </summary>
[ApiExplorerSettings(GroupName = SwaggerConfig.System)]
[Authorize]
[Route("api/file")]
public class FileController : ControllerBase
{
	//readonly IFileService _service;

	/// <summary>
	/// ���캯��
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="service"></param>
	public FileController(ILogger<FileController> logger) 
	{
		//_service = service;
	}

	/// <summary>
	/// �ϴ�
	/// </summary>
	/// <param name="files">�ļ����ļ���name���Ա�����files</param>
	/// <returns></returns>
	[HttpPost("upload")]
	public async Task<ApiResult<ICollection<FileOutput>>> UploadAsync([FromForm] IFormFile[] files)
	{
		//var res = await _service.UploadAsync(files);
		//return ApiResult.Success(null);
		return null;
	}
}