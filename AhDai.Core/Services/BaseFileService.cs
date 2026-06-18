using AhDai.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// 文件服务
/// </summary>
/// <param name="options"></param>
/// <param name="httpClientFactory"></param>
public class BaseFileService(IOptionsMonitor<Options.FileOptions> options, IHttpClientFactory? httpClientFactory) : IBaseFileService
{
    readonly IOptionsMonitor<Options.FileOptions> _options = options;
    readonly IHttpClientFactory? _httpClientFactory = httpClientFactory;
    readonly FileExtensionContentTypeProvider _contentTypeProvider = new();

    /// <summary>
    /// TypeProvider
    /// </summary>
    public FileExtensionContentTypeProvider ContentTypeProvider => _contentTypeProvider;
    /// <summary>
    /// httpClientFactory
    /// </summary>
    public IHttpClientFactory HttpClientFactory => _httpClientFactory ?? throw new InvalidOperationException("未注入IHttpClientFactory");

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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<Models.FileData> UploadAsync(string root, string dir, IFormFile file, string? category = null, CancellationToken cancellationToken = default)
    {
        var result = await UploadAsync(root, dir, [file], category, cancellationToken);
        return result[0];
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="root"></param>
    /// <param name="dir"></param>
    /// <param name="files"></param>
    /// <param name="category"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<Models.FileData[]> UploadAsync(string root, string dir, IFormFile[] files, string? category = null, CancellationToken cancellationToken = default)
    {
        //If the request is correct, the binary data will be extracted from content and IIS stores files in specified location.
        if (files.Length == 0)
        {
            throw new ArgumentException("没有需要上传的文件");
        }

        var options = _options.CurrentValue;
        var virDir = $"{options.UploadDirectory}/{dir}";
        var phyDir = Path.Combine(root, options.UploadDirectory, dir);
        Directory.CreateDirectory(phyDir);

        var extensions = string.IsNullOrEmpty(category) ? [.. options.Extensions.SelectMany(x => x.Value)] : options.Extensions.GetValueOrDefault(category) ?? throw new ArgumentException($"未知文件分类:{category}"); ;
        var datas = new Models.FileData[files.Length];
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!extensions.Contains(extension, StringComparer.OrdinalIgnoreCase)) throw new ArgumentException($"不支持的文件类型：{extension}");
            if (file.Length > options.MaxLength) throw new ArgumentException($"超出文件大小限制：{Utils.FileUtil.GetFileSize(options.MaxLength)}");

            var actualName = $"{Guid.NewGuid()}{extension}";
            var actualPath = Path.Combine(phyDir, actualName);

            using var stream = file.OpenReadStream();
            using var fs = new FileStream(actualPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192, true);
            await stream.CopyToAsync(fs, cancellationToken);
            await fs.FlushAsync(cancellationToken);

            var hash = await ComputeHashAsync(fs, options.ComputeHash, cancellationToken);
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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Models.FileData> DownloadAsync(HttpClient? httpClient, string root, string dir, string url, string? name = null, CancellationToken cancellationToken = default)
    {
        var (data, fs) = await DownloadAndOpenAsync(httpClient, root, dir, url, name, cancellationToken);
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
    /// <param name="cancellationToken"></param>
    /// <returns>fs的Position=0</returns>
    public async Task<(Models.FileData, FileStream)> DownloadAndOpenAsync(HttpClient? httpClient, string root, string dir, string url, string? name = null, CancellationToken cancellationToken = default)
    {
        httpClient ??= HttpClientFactory.CreateClient();

        using var res = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        res.EnsureSuccessStatusCode();

        var disposition = res.Content.Headers.ContentDisposition;

        if (string.IsNullOrEmpty(name))
        {
            name = disposition?.FileNameStar ?? disposition?.FileName?.Trim('"') ?? name;

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
        }
        var extension = Path.GetExtension(name).ToLowerInvariant();

        var options = _options.CurrentValue;
        var virDir = $"{options.DownloadDirectory}/{dir}";
        var phyDir = Path.Combine(root, options.DownloadDirectory, dir);
        Directory.CreateDirectory(phyDir);

        var actualName = $"{Guid.NewGuid()}{extension}";
        var actualPath = Path.Combine(phyDir, actualName);

        long length;
        string hash;
        FileStream? fs = null;
        try
        {
            using var stream = await res.Content.ReadAsStreamAsync(cancellationToken);
            fs = new FileStream(actualPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192, true);
            await stream.CopyToAsync(fs, cancellationToken);
            await fs.FlushAsync(cancellationToken);

            length = fs.Length;
            hash = await ComputeHashAsync(fs, options.ComputeHash, cancellationToken);
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
    /// 计算哈希
    /// </summary>
    /// <param name="fs"></param>
    /// <param name="computeHash"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<string> ComputeHashAsync(Stream fs, bool? computeHash = null, CancellationToken cancellationToken = default)
    {
        var options = _options.CurrentValue;
        if (!(computeHash ?? options.ComputeHash)) return "";

        if (!fs.CanSeek) throw new NotSupportedException("Stream must support seek");
        fs.Position = 0;
        var hashBytes = await SHA256.HashDataAsync(fs, cancellationToken);
        fs.Position = 0;
#if NET9_0_OR_GREATER
        return Convert.ToHexStringLower(hashBytes);
#else
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
#endif
    }

    /// <summary>
    /// 计算哈希
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="computeHash"></param>
    /// <returns></returns>
    public virtual string ComputeHash(byte[] bytes, bool? computeHash = null)
    {
        var options = _options.CurrentValue;
        if (!(computeHash ?? options.ComputeHash)) return "";

        var hashBytes = SHA256.HashData(bytes);
#if NET9_0_OR_GREATER
        return Convert.ToHexStringLower(hashBytes);
#else
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
#endif
    }
}
