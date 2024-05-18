namespace AhDai.Entity;

/// <summary>
/// IParentIdEntity
/// </summary>
public interface IParentIdEntity
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 父级Id
    /// </summary>
    public long ParentId { get; set; }
}
