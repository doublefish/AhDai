namespace AhDai.Entity.Sys;

/// <summary>
/// 组织
/// </summary>
public class Organization : BaseTenantEntity, ICodeEntity, IParentIdEntity, IUniqueCodeEntity
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
    /// 层级
    /// </summary>
    public int Level { get; set; }
    /// <summary>
    /// 序号
    /// </summary>
    public int Number { get; set; }
    /// <summary>
    /// 唯一编码
    /// </summary>
    public string UniqueCode { get; set; } = "";
    /// <summary>
    /// 负责人Id
    /// </summary>
    public long? LeaderId { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 顺序
    /// </summary>
    public int Sort { get; set; }
    /// <summary>
    /// 状态：1-正常，2-停用
    /// </summary>
    public Shared.Enums.GenericStatus Status { get; set; }
}
