﻿using System.Collections.Generic;

namespace AhDai.Core.Configs;

/// <summary>
/// 文件配置
/// </summary>
public class FileConfig
{
    /// <summary>
    /// 上传根目录
    /// </summary>
    public string UploadDirectory { get; set; } = "upload";
    /// <summary>
    /// 下载根目录
    /// </summary>
    public string DownloadDirectory { get; set; } = "download";
    /// <summary>
    /// 最大长度
    /// </summary>
    public long MaxLength { get; set; }
    /// <summary>
    /// 扩展名
    /// </summary>
    public IDictionary<string, string[]> Extensions { get; set; } = new Dictionary<string, string[]>();

}
