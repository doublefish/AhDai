namespace AhDai.Service.Models;

/// <summary>
/// 文件
/// </summary>
public class FileInput : BaseInput
{
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 路径
	/// </summary>
	public string Path { get; set; }
}
