namespace AhDai.Service.Sys.Models;

/// <summary>
/// 文件查询入参
/// </summary>
public class FileQueryInput : BaseQueryInput
{
    /// <summary>
    /// 名称：全模糊
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 扩展名
    /// </summary>
    public string? Extension { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public string? Type { get; set; }
	/// <summary>
	/// 类型
	/// </summary>
	public string[]? Types { get; set; }
}
