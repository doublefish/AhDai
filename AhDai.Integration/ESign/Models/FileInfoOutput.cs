namespace AhDai.Integration.ESign.Models;

/// <summary>
/// FileInfoOutput
/// </summary>
public class FileInfoOutput
{
    /// <summary>
    /// 文件ID
    /// </summary>
    public string FileId { get; set; } = default!;
    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; } = default!;
    /// <summary>
    /// 文件状态：
    /// 0 - 文件未上传
    /// 1 - 文件上传中
    /// 2 - 文件上传已完成 或 文件已转换（HTML）
    /// 3 - 文件上传失败
    /// 4 - 文件等待转换（PDF）
    /// 5 - 文件已转换（PDF）6 - 加水印中
    /// 7 - 加水印完毕
    /// 8 - 文件转化中（PDF）
    /// 9 - 文件转换失败（PDF）
    /// 10 - 文件等待转换（HTML）
    /// 11 - 文件转换中（HTML）
    /// 12 - 文件转换失败（HTML）
    /// </summary>
    public int FileStatus { get; set; }
    /// <summary>
    /// 文件下载地址：有效期60分钟
    /// </summary>
    public string FileDownloadUrl { get; set; } = default!;
    /// <summary>
    /// pdf文件总页数
    /// </summary>
    public int FileTotalPageCount { get; set; }
    /// <summary>
    /// 首页宽度，单位：像素（px）
    /// 【注】pageSize传true才返回具体值
    /// </summary>
    public float? PageWidth { get; set; }
    /// <summary>
    /// 首页高度，单位：像素（px）
    /// 【注】pageSize传true才返回具体值
    /// </summary>
    public float? PageHeight { get; set; }
}
