using System;

namespace AhDai.Service.Models;

/// <summary>
/// 文件
/// </summary>
public class FileOutput : BaseOutput
{
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 扩展名
	/// </summary>
	public string Extension { get; set; }
	/// <summary>
	/// 类型
	/// </summary>
	public string Type { get; set; }
	/// <summary>
	/// 大小
	/// </summary>
	public long Length { get; set; }
	/// <summary>
	/// 相对路径
	/// </summary>
	public string RelativePath { get; set; }
	/// <summary>
	/// 路径
	/// </summary>
	public string Path { get; set; }
	/// <summary>
	/// 哈希
	/// </summary>
	public string Hash { get; set; }
}
