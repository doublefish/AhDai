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
using System.Security.Cryptography;
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

        var virDir = $"{_config.UploadDirectory}/{dir}";
        var phyDir = Path.Combine(root, _config.UploadDirectory, dir);
        if (!Directory.Exists(phyDir))
        {
            Directory.CreateDirectory(phyDir);
        }

        var extensions = string.IsNullOrEmpty(category) ? _config.Extensions.SelectMany(x => x.Value).ToArray() : _config.Extensions[category];
        var datas = new Models.FileData[files.Length];
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!extensions.Contains(extension, StringComparer.OrdinalIgnoreCase)) throw new ArgumentException($"不支持的文件类型：{extension}");
            if (file.Length > _config.MaxLength) throw new ArgumentException($"超出文件大小限制：{Utils.FileUtil.GetFileSize(_config.MaxLength)}");

            var actualName = $"{Guid.NewGuid()}{extension}";
            var actualPath = Path.Combine(phyDir, actualName);

            using var stream = file.OpenReadStream();
            using var fs = new FileStream(actualPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192, true);
            await stream.CopyToAsync(fs);
            await fs.FlushAsync();

            var hash = await ComputeHashAsync(fs);
            datas[i] = new Models.FileData()
            {
                Name = Path.GetFileName(file.FileName),
                ActualName = actualName,
                ActualPath = actualPath,
                Extension = extension,
                Type = GetType(extension),
                Length = file.Length,
                Hash = hash,
                VirtualDirectory = virDir,
                PhysicalDirectory = phyDir,
            };
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
        var (data, fs) = await DownloadAndOpenAsync(httpClient, root, dir, url, name);
        await fs.DisposeAsync();
        return data;
    }

    /// <summary>
    /// 下载并打开
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="url"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<(Models.FileData, FileStream)> DownloadAndOpenAsync(HttpClient? httpClient, string root, string dir, string url, string? name = null)
    {
        httpClient ??= HttpClientFactory.CreateClient();

        if (string.IsNullOrEmpty(name))
        {
            //var uri = new Uri(url);
            //name = Path.GetFileName(uri.AbsolutePath);
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                name = Path.GetFileName(uri.AbsolutePath);
            }
            else
            {
                name = Path.GetFileName(url);
            }
        }
        var extension = Path.GetExtension(name).ToLowerInvariant();

        var virDir = $"{_config.DownloadDirectory}/{dir}";
        var phyDir = Path.Combine(root, _config.DownloadDirectory, dir);
        if (!Directory.Exists(phyDir))
        {
            Directory.CreateDirectory(phyDir);
        }

        var actualName = $"{Guid.NewGuid()}{extension}";
        var actualPath = Path.Combine(phyDir, actualName);

        using var res = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        res.EnsureSuccessStatusCode();

        long length;
        string hash;
        FileStream? fs = null;
        try
        {
            using var stream = await res.Content.ReadAsStreamAsync();
            fs = new FileStream(actualPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192, true);
            await stream.CopyToAsync(fs);
            await fs.FlushAsync();

            length = fs.Length;
            hash = await ComputeHashAsync(fs);
        }
        catch
        {
            if (fs != null)
            {
                await fs.DisposeAsync();
            }
            if (File.Exists(actualPath))
            {
                File.Delete(actualPath);
            }
            throw;
        }

        return (new Models.FileData()
        {
            Name = Path.GetFileName(name),
            ActualName = actualName,
            ActualPath = actualPath,
            Extension = extension,
            Type = GetType(extension),
            Length = length,
            Hash = hash,
            VirtualDirectory = virDir,
            PhysicalDirectory = phyDir,
        }, fs);
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
        var folderPath = Path.Combine(rootPath, _config.DownloadDirectory);
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

    /// <summary>
    /// 计算哈希
    /// </summary>
    /// <param name="fs"></param>
    /// <param name="computeHash"></param>
    /// <returns></returns>
    public virtual async Task<string> ComputeHashAsync(Stream fs, bool? computeHash = null)
    {
        if (!(computeHash ?? _config.ComputeHash)) return "";

        fs.Seek(0, SeekOrigin.Begin);
        var hashBytes = await SHA256.HashDataAsync(fs);
        fs.Seek(0, SeekOrigin.Begin);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// 计算哈希
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="computeHash"></param>
    /// <returns></returns>
    public virtual string ComputeHash(byte[] bytes, bool? computeHash = null)
    {
        if (!(computeHash ?? _config.ComputeHash)) return "";

        var hashBytes = SHA256.HashData(bytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}
