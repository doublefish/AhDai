using System.Collections.Generic;

namespace AhDai.Core.Configs;

/// <summary>
/// 文件配置
/// </summary>
public class FileConfig
{
    /// <summary>
    /// 上传根目录
    /// </summary>
    public string UploadDirectory { get; set; } = "uploads";
    /// <summary>
    /// 下载根目录
    /// </summary>
    public string DownloadDirectory { get; set; } = "downloads";
    /// <summary>
    /// 文件导出目录
    /// </summary>
    public string ExportDirectory { get; set; } = "exports";
    /// <summary>
    /// 最大长度
    /// </summary>
    public long MaxLength { get; set; }
    /// <summary>
    /// 扩展名
    /// </summary>
    public IDictionary<string, string[]> Extensions { get; set; } = new Dictionary<string, string[]>();
    /// <summary>
    /// 是否需要计算文件哈希（SHA256）
    /// </summary>
    public bool ComputeHash { get; set; }
}
