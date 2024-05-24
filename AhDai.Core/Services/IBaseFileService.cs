using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// IBaseFileService
/// </summary>
public interface IBaseFileService
{
    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="formFiles"></param>
    /// <returns></returns>
    ICollection<Models.FileData> Upload(string rootPath, params IFormFile[] formFiles);

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="formFiles"></param>
    /// <returns></returns>
    Task<ICollection<Models.FileData>> UploadAsync(string rootPath, params IFormFile[] formFiles);
}
