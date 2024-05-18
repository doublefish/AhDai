using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 菜单
/// </summary>
public class MenuInput : BaseInput
{
	/// <summary>
	/// 编码
	/// </summary>
	[Required]
	public string Code { get; set; } = "";
	/// <summary>
	/// 名称
	/// </summary>
	[Required]
	public string Name { get; set; } = "";
	/// <summary>
	/// 父级Id
	/// </summary>
	[Required]
	public long ParentId { get; set; }
	/// <summary>
	/// 类型
	/// </summary>
	[Required]
	public Shared.Enums.MenuType Type { get; set; }
	/// <summary>
	/// 图标
	/// </summary>
	public string Icon { get; set; } = "";
    /// <summary>
    /// 路由
    /// </summary>
    public string Router { get; set; } = "";
    /// <summary>
    /// 重定向
    /// </summary>
    public string Redirect { get; set; } = "";
    /// <summary>
    /// 组件
    /// </summary>
    public string Component { get; set; } = "";
    /// <summary>
    /// 权限标识
    /// </summary>
    public string Permission { get; set; } = "";
	/// <summary>
	/// 应用编码
	/// </summary>
	public string Application { get; set; } = "";
	/// <summary>
	/// 表单Id
	/// </summary>
	public long? FormId { get; set; }
	/// <summary>
	/// 是否隐藏
	/// </summary>
	public bool Hidden { get; set; }
	/// <summary>
	/// 备注
	/// </summary>
	public string? Remark { get; set; }
	/// <summary>
	/// 顺序：0-999
	/// </summary>
	[Range(0, 999)]
	public int Sort { get; set; } = 999;
}
