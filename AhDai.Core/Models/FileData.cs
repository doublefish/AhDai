﻿namespace AhDai.Core.Models;

/// <summary>
/// 上传文件
/// </summary>
public class FileData
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public string Guid { get; set; } = default!;
    /// <summary>
    /// 文件名
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 后缀名
    /// </summary>
    public string Extension { get; set; } = default!;
    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; } = default!;
    /// <summary>
    /// 大小
    /// </summary>
    public long Length { get; set; }
    /// <summary>
    /// 物理路径
    /// </summary>
    public string PhysicalPath { get; set; } = default!;
    /// <summary>
    /// 虚拟路径
    /// </summary>
    public string VirtualPath { get; set; } = default!;
    /// <summary>
    /// 哈希
    /// </summary>
    public string Hash { get; set; } = default!;
}