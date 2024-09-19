﻿using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// IBaseFileService
/// </summary>
public interface IBaseFileService
{
    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="extension"></param>
    /// <returns>MimeType</returns>
    string GetType(string extension);

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="file"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    Task<Models.FileData> UploadAsync(string root, string dir, IFormFile file, string? category = null);

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="files"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    Task<Models.FileData[]> UploadAsync(string root, string dir, IFormFile[] files, string? category = null);

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="url"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<Models.FileData> DownloadAsync(string root, string dir, string url, string name);

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="url"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<Models.FileData> DownloadAsync(HttpClient httpClient, string root, string dir, string url, string name);
}
