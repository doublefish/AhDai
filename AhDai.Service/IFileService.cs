﻿using AhDai.Service.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IFileService
/// </summary>
public interface IFileService : IBaseService<int, FileOutput, FileQueryInput>
{
	/// <summary>
	/// 上传
	/// </summary>
	/// <param name="files"></param>
	/// <returns></returns>
	Task<ICollection<FileOutput>> UploadAsync(params IFormFile[] files);
}
