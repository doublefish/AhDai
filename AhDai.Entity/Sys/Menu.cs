namespace AhDai.Entity.Sys;

/// <summary>
/// 菜单
/// </summary>
public class Menu : BaseEntity, ICodeEntity, IParentIdEntity
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = "";
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// 父级Id
    /// </summary>
    public long ParentId { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
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
    /// 顺序
    /// </summary>
    public int Sort { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public Shared.Enums.GenericStatus Status { get; set; }
}
