using Microsoft.AspNetCore.Http;
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
    /// <param name="file"></param>
    /// <param name="fileType"></param>
    /// <returns></returns>
    Task<Models.FileData> UploadAsync(string rootPath, IFormFile file, string? fileType = null);

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="files"></param>
    /// <param name="fileType"></param>
    /// <returns></returns>
    Task<Models.FileData[]> UploadAsync(string rootPath, IFormFile[] files, string? fileType = null);

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
