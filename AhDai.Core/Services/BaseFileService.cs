using AhDai.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// 文件服务
/// </summary>
/// <param name="configuration"></param>
/// <param name="httpClientFactory"></param>
public class BaseFileService(IConfiguration configuration, IHttpClientFactory? httpClientFactory) : IBaseFileService
{
    readonly IHttpClientFactory? _httpClientFactory = httpClientFactory;
    readonly Configs.FileConfig _config = configuration.GetFileConfig();
    readonly FileExtensionContentTypeProvider _contentTypeProvider = new();

    /// <summary>
    /// Config
    /// </summary>
    public Configs.FileConfig Config => _config;
    /// <summary>
    /// TypeProvider
    /// </summary>
    public FileExtensionContentTypeProvider ContentTypeProvider => _contentTypeProvider;
    /// <summary>
    /// httpClientFactory
    /// </summary>
    public IHttpClientFactory HttpClientFactory => _httpClientFactory ?? throw new ArgumentException("未注入IHttpClientFactory");

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="extension"></param>
    /// <returns>MimeType</returns>
    public string GetType(string extension)
    {
        _contentTypeProvider.TryGetContentType(extension, out var type);
        return type ?? "application/octet-stream";
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="file"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    public virtual async Task<Models.FileData> UploadAsync(string root, string dir, IFormFile file, string? category = null)
    {
        var result = await UploadAsync(root, dir, [file], category);
        return result[0];
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="files"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    public virtual async Task<Models.FileData[]> UploadAsync(string root, string dir, IFormFile[] files, string? category = null)
    {
        files = files.Where(o => o != null).ToArray();
        //If the request is correct, the binary data will be extracted from content and IIS stores files in specified location.
        if (files.Length == 0)
        {
            throw new ArgumentException("没有需要上传的文件");
        }

        var virDir = $"{Config.UploadDirectory}/{dir}";
        var phyDir = Path.Combine(root, Config.UploadDirectory, dir);
        if (!Directory.Exists(phyDir))
        {
            Directory.CreateDirectory(phyDir);
        }

        var extensions = string.IsNullOrEmpty(category) ? Config.Extensions.SelectMany(x => x.Value).ToArray() : Config.Extensions[category];
        var datas = new Models.FileData[files.Length];
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!extensions.Contains(extension)) throw new ArgumentException($"不支持的文件类型：{extension}");
            if (file.Length > Config.MaxLength) throw new ArgumentException($"超出文件大小限制：{Utils.FileUtil.GetFileSize(Config.MaxLength)}");

            var actualName = $"{Guid.NewGuid()}{extension}";
            datas[i] = new Models.FileData()
            {
                Name = Path.GetFileName(file.FileName),
                ActualName = actualName,
                ActualPath = Path.Combine(phyDir, actualName),
                Extension = extension,
                Length = file.Length,
                Type = GetType(extension),
                VirtualDirectory = virDir,
                PhysicalDirectory = phyDir,
            };
        }
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var data = datas[i];
            using var stream = new FileStream(data.ActualPath, FileMode.Create);
            await file.CopyToAsync(stream);
            //var hashBytes = System.Security.Cryptography.SHA1.HashData(stream);
            //data.Hash = BitConverter.ToString(hashBytes).Replace("-", "");
        }
        return datas;
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="url"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<Models.FileData> DownloadAsync(HttpClient? httpClient, string root, string dir, string url, string? name = null)
    {
        httpClient ??= HttpClientFactory.CreateClient();
        //name ??= Path.GetFileName(url);
        if (string.IsNullOrEmpty(name))
        {
            var uri = new Uri(url);
            name = Path.GetFileName(uri.AbsolutePath);
        }
        var extension = Path.GetExtension(name).ToLowerInvariant();

        var virDir = $"{Config.DownloadDirectory}/{dir}";
        var phyDir = Path.Combine(root, Config.DownloadDirectory, dir);
        if (!Directory.Exists(phyDir))
        {
            Directory.CreateDirectory(phyDir);
        }

        var actualName = $"{Guid.NewGuid()}{extension}";
        var data = new Models.FileData()
        {
            Name = Path.GetFileName(name),
            ActualName = actualName,
            ActualPath = Path.Combine(phyDir, actualName),
            Extension = extension,
            //Length = formFile.Length,
            Type = GetType(extension),
            VirtualDirectory = virDir,
            PhysicalDirectory = phyDir,
        };

        using var res = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        res.EnsureSuccessStatusCode();
        await using (var cs = await res.Content.ReadAsStreamAsync())
        {
            using var fs = new FileStream(data.ActualPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            await cs.CopyToAsync(fs);
            data.Length = fs.Length;
        }
        return data;
    }

    /// <summary>
    /// 批量压缩
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="files"></param>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    public virtual string Compress(string rootPath, IDictionary<string, string> files)
    {
        ArgumentNullException.ThrowIfNull(files);
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        //文件目录
        var folderPath = Path.Combine(rootPath, Config.DownloadDirectory);
        //临时文件夹路径
        var tempFolderPath = Path.Combine(folderPath, timestamp);
        //创建临时文件夹
        Directory.CreateDirectory(tempFolderPath);

        foreach (var kv in files)
        {
            var sourceFileName = kv.Value;
            if (!File.Exists(sourceFileName))
            {
                continue;
            }
            var destFileName = Path.Combine(tempFolderPath, kv.Key);
            File.Copy(sourceFileName, destFileName);
        }

        //生成RAR文件
        var fileName = $"{timestamp}.rar";
        Utils.RARUtil.Compress(tempFolderPath, folderPath, fileName);
        //清空临时文件夹
        Directory.Delete(tempFolderPath, true);
        var filePath = Path.Combine(folderPath, fileName);
        return filePath;
    }
}
