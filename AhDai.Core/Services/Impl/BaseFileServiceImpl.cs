using AhDai.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace AhDai.Core.Services.Impl;

/// <summary>
/// FileService
/// </summary>
public class BaseFileServiceImpl : IBaseFileService
{
    /// <summary>
    /// Config
    /// </summary>
    public Configs.FileConfig Config { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    public BaseFileServiceImpl(IConfiguration configuration)
    {
        Config = configuration.GetFileConfig();
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="formFiles"></param>
    /// <returns></returns>
    public virtual ICollection<Models.FileData> Upload(string rootPath, params IFormFile[] formFiles)
    {
        var task = Task.Run(() =>
        {
            return UploadAsync(rootPath, formFiles);
        });
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="formFiles"></param>
    /// <returns></returns>
    public virtual async Task<ICollection<Models.FileData>> UploadAsync(string rootPath, params IFormFile[] formFiles)
    {
        formFiles = formFiles.Where(o => o != null).ToArray();
        //If the request is correct, the binary data will be extracted from content and IIS stores files in specified location.
        if (formFiles.Length == 0)
        {
            throw new Exception("没有需要上传的文件");
        }

        //虚拟目录
        var dir = DateTime.Now.ToString("yyyy-MM-dd");
        var virtualDir = $"{Config.UploadDirectory}/{dir}";
        //物理路径
        var physicalPath = Path.Combine(rootPath, Config.UploadDirectory, dir);
        if (!Directory.Exists(physicalPath))
        {
            Directory.CreateDirectory(physicalPath);
        }
        var datas = new HashSet<Models.FileData>();
        var temps = new List<Tuple<Models.FileData, IFormFile>>();
        foreach (var formFile in formFiles)
        {
            var data = new Models.FileData()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = Path.GetFileNameWithoutExtension(formFile.FileName),
                Extension = Path.GetExtension(formFile.FileName).ToLowerInvariant(),
                Length = formFile.Length
            };
            var extensions = Config.Extensions.Where(o => o.Value.Contains(data.Extension)).FirstOrDefault();
            if (extensions.Key == null)
            {
                throw new Exception("不支持的文件类型：" + data.Extension);
            }
            data.Type = extensions.Key;
            if (formFile.Length > Config.MaxSize)
            {
                throw new Exception($"超出文件大小限制：{Config.MaxSizeNote}");
            }
            //物理路径
            data.PhysicalPath = Path.Combine(physicalPath, data.FullName);
            //虚拟路径
            data.VirtualPath = $"{virtualDir}/{data.FullName}";
            datas.Add(data);
            temps.Add(new Tuple<Models.FileData, IFormFile>(data, formFile));
        }
        foreach (var temp in temps)
        {
            var data = temp.Item1;
            var formFile = temp.Item2;
            using var stream = new FileStream(data.PhysicalPath, FileMode.Create);
            //await formFile.CopyToAsync(stream).ConfigureAwait(false);
            await formFile.CopyToAsync(stream);
            //var hashBytes = System.Security.Cryptography.SHA1.HashData(stream);
            //data.Hash = BitConverter.ToString(hashBytes).Replace("-", "");
        }
        return datas;
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
        if (files == null)
        {
            throw new ArgumentNullException(nameof(files));
        }
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
