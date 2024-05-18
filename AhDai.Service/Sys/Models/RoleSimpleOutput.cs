namespace AhDai.Service.Sys.Models;

/// <summary>
/// 角色
/// </summary>
public class RoleSimpleOutput
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = "";
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// 类型：0-普通，1-内置
    /// </summary>
    public int Type { get; set; }
}
