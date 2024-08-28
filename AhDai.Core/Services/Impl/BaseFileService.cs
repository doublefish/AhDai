using AhDai.Core.Extensions;
using AhDai.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace AhDai.Core.Services.Impl;

/// <summary>
/// 文件服务
/// </summary>
/// <param name="configuration"></param>
public class BaseFileService(IConfiguration configuration) : IBaseFileService
{
    /// <summary>
    /// Config
    /// </summary>
    public Configs.FileConfig Config { get; private set; } = configuration.GetFileConfig();

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="file"></param>
    /// <param name="fileType"></param>
    /// <returns></returns>
    public virtual async Task<Models.FileData> UploadAsync(string rootPath, IFormFile file, string? fileType = null)
    {
        var result = await UploadAsync(rootPath, [file], fileType);
        return result[0];
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="files"></param>
    /// <param name="fileType"></param>
    /// <returns></returns>
    public virtual async Task<Models.FileData[]> UploadAsync(string rootPath, IFormFile[] files, string? fileType = null)
    {
        files = files.Where(o => o != null).ToArray();
        //If the request is correct, the binary data will be extracted from content and IIS stores files in specified location.
        if (files.Length == 0)
        {
            throw new ArgumentException("没有需要上传的文件");
        }

        var dir = DateTime.Now.ToString("yyyy-MM-dd");
        var virDir = $"{Config.UploadDirectory}/{dir}";
        var phyDir = Path.Combine(rootPath, Config.UploadDirectory, dir);
        if (!Directory.Exists(phyDir))
        {
            Directory.CreateDirectory(phyDir);
        }
        var datas = new Models.FileData[files.Length];
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var type = "";
            if (!string.IsNullOrEmpty(fileType))
            {
                if (!Config.Extensions.TryGetValue(fileType, out var extensions)) throw new ArgumentException($"不支持的文件类别");
                if (!extensions.Contains(extension)) throw new ArgumentException($"不支持的文件类型：{extension}");
                type = fileType;
            }
            else
            {
                var exts = Config.Extensions.Where(o => o.Value.Contains(extension)).FirstOrDefault();
                if (exts.Key == null) throw new ArgumentException($"不支持的文件类型：{extension}");
                type = exts.Key;
            }
            if (file.Length > Config.MaxSize) throw new ArgumentException($"超出文件大小限制：{Config.MaxSizeNote}");

            var guid = Guid.NewGuid().ToString();
            var saveName = $"{guid}{extension}";
            datas[i] = new Models.FileData()
            {
                Guid = guid,
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                Extension = extension,
                FullName = Path.GetFileName(file.FileName),
                Length = file.Length,
                Type = type,
                PhysicalPath = Path.Combine(phyDir, saveName),
                VirtualPath = $"{virDir}/{saveName}",
            };
        }
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            var data = datas[i];
            using var stream = new FileStream(data.PhysicalPath, FileMode.Create);
            await file.CopyToAsync(stream);
            //var hashBytes = System.Security.Cryptography.SHA1.HashData(stream);
            //data.Hash = BitConverter.ToString(hashBytes).Replace("-", "");
        }
        return datas;
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="fileName"></param>
    /// <param name="fileUrl"></param>
    /// <returns></returns>
    public async Task<Models.FileData> DownloadAsync(string rootPath, string fileName, string fileUrl)
    {
        var httpClientFactory = ServiceUtil.Services.GetRequiredService<IHttpClientFactory>();
        using var httpClient = httpClientFactory.CreateClient();
        return await DownloadAsync(httpClient, rootPath, fileName, fileUrl);
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="rootPath"></param>
    /// <param name="fileName"></param>
    /// <param name="fileUrl"></param>
    /// <returns></returns>
    public async Task<Models.FileData> DownloadAsync(HttpClient httpClient, string rootPath, string fileName, string fileUrl)
    {
        var dir = DateTime.Now.ToString("yyyy-MM-dd");
        var virDir = $"{Config.DownloadDirectory}/{dir}";
        var phyDir = Path.Combine(rootPath, Config.DownloadDirectory, dir);
        if (!Directory.Exists(phyDir))
        {
            Directory.CreateDirectory(phyDir);
        }
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        var exts = Config.Extensions.Where(o => o.Value.Contains(ext)).FirstOrDefault();
        var guid = Guid.NewGuid().ToString();
        var saveName = $"{guid}{ext}";
        var data = new Models.FileData()
        {
            Guid = guid,
            Name = Path.GetFileNameWithoutExtension(fileName),
            Extension = ext,
            FullName = fileName,
            //Length = formFile.Length,
            Type = exts.Key,
            PhysicalPath = Path.Combine(phyDir, saveName),
            VirtualPath = $"{virDir}/{saveName}",
        };

        using var res = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
        res.EnsureSuccessStatusCode();
        await using (var cs = await res.Content.ReadAsStreamAsync())
        {
            using var fs = new FileStream(data.PhysicalPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            await cs.CopyToAsync(fs);
            data.Length = cs.Length;
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
