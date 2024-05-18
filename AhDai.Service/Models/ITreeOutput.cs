namespace AhDai.Service.Models;

/// <summary>
/// ITreeModel
/// </summary>
public interface ITreeOutput<T>
    where T : ITreeOutput<T>
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
    /// 子节点
    /// </summary>
    public T[]? Children { get; set; }
}
