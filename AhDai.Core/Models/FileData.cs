namespace AhDai.Core.Models;

/// <summary>
/// 上传文件
/// </summary>
public class FileData
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// 实际名称
    /// </summary>
    public string ActualName { get; set; } = null!;
    /// <summary>
    /// 后缀名
    /// </summary>
    public string Extension { get; set; } = null!;
    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; } = null!;
    /// <summary>
    /// 大小
    /// </summary>
    public long Length { get; set; }
    /// <summary>
    /// 哈希
    /// </summary>
    public string Hash { get; set; } = null!;
    /// <summary>
    /// 虚拟目录
    /// </summary>
    public string VirtualDirectory { get; set; } = null!;
    /// <summary>
    /// 物理目录
    /// </summary>
    public string PhysicalDirectory { get; set; } = null!;
}