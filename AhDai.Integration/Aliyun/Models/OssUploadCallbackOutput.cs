namespace AhDai.Integration.Aliyun.Models;

/// <summary>
/// OssUploadCallbackOutput
/// </summary>
public class OssUploadCallbackOutput
{
    /// <summary>
    /// Bucket
    /// </summary>
    public string Bucket { get; set; } = default!;
    /// <summary>
    /// Region
    /// </summary>
    public string Region { get; set; } = default!;
    /// <summary>
    /// 文件名
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 对象全名
    /// </summary>
    public string Object { get; set; } = default!;
    /// <summary>
    /// MimeType
    /// </summary>
    public string MimeType { get; set; } = default!;
    /// <summary>
    /// 文件长度
    /// </summary>
    public long Length { get; set; }
    /// <summary>
    /// 哈希
    /// </summary>
    public string Hash { get; set; } = default!;
    /// <summary>
    /// 宽度
    /// </summary>
    public int? Width { get; set; }
    /// <summary>
    /// 高度
    /// </summary>
    public int? Height { get; set; }
}
