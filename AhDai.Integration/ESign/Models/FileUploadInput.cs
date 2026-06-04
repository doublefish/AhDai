using System.IO;
using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 文件上传入参
/// </summary>
public class FileUploadInput
{
    /// <summary>
    /// 目标文件的MIME类型
    /// 可填写 application/octet-stream 或 application/pdf
    /// </summary>
    public string? ContentType { get; set; }
    /// <summary>
    /// 文件名称（必须带上文件扩展名）
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// 文件链接（远程地址）和 FilePath 二选一
    /// </summary>
    public string? FileUrl { get; set; }
    /// <summary>
    /// 文件地址（本地地址）和 FileUrl 二选一
    /// </summary>
    public string? FilePath { get; set; }
    /// <summary>
    /// 文件流
    /// </summary>
    [JsonIgnore]
    public FileStream? FileStream { get; set; }
    /// <summary>
    /// 是否需要转换成PDF文档，默认值 false
    /// </summary>
    public bool? ConvertToPDF { get; set; }
    /// <summary>
    /// 是否需要转换成HTML文档，默认值 false
    /// </summary>
    public bool? ConvertToHTML { get; set; }
    /// <summary>
    /// 专属云项目ID
    /// </summary>
    public string? DedicatedCloudId { get; set; }
}
