using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
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
    Task<ICollection<Models.FileData>> UploadAsync(string rootPath, params IFormFile[] formFiles);

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="fileName"></param>
    /// <param name="fileUrl"></param>
    /// <returns></returns>
    Task<Models.FileData> DownloadAsync(string rootPath, string fileName, string fileUrl);

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="rootPath"></param>
    /// <param name="fileName"></param>
    /// <param name="fileUrl"></param>
    /// <returns></returns>
    Task<Models.FileData> DownloadAsync(HttpClient httpClient, string rootPath, string fileName, string fileUrl);
}
