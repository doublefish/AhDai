namespace AhDai.Entity;

/// <summary>
/// UniqueCodeEntity
/// </summary>
public class UniqueCodeEntity : IUniqueCodeEntity
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
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
    public string UniqueCode { get; set; } = default!;

}
