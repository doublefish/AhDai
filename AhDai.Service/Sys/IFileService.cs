using AhDai.Service.Sys.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AhDai.Service.Sys;

/// <summary>
/// IFileService
/// </summary>
public interface IFileService : IBaseService<FileInput, FileOutput, FileQueryInput>
{
	/// <summary>
	/// 上传
	/// </summary>
	/// <param name="file"></param>
	/// <returns></returns>
	Task<FileOutput> UploadAsync(IFormFile file);

	/// <summary>
	/// 上传
	/// </summary>
	/// <param name="files"></param>
	/// <returns></returns>
	Task<FileOutput[]> UploadAsync(params IFormFile[] files);
}
