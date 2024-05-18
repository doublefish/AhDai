namespace AhDai.Service.Sys.Models;

/// <summary>
/// 菜单查询入参
/// </summary>
public class MenuQueryInput : BaseQueryInput
{
	/// <summary>
	/// 关键字
	/// </summary>
	public string? Keyword { get; set; }
	/// <summary>
	/// 编码
	/// </summary>
	public string? Code { get; set; }
	/// <summary>
	/// 编码
	/// </summary>
	public string[]? Codes { get; set; }
	/// <summary>
	/// 名称：全模糊
	/// </summary>
	public string? Name { get; set; }
	/// <summary>
	/// 父级Id
	/// </summary>
	public long? ParentId { get; set; }
	/// <summary>
	/// 类型
	/// </summary>
	public Shared.Enums.MenuType? Type { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public Shared.Enums.MenuType[]? Types { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public Shared.Enums.GenericStatus? Status { get; set; }
}
