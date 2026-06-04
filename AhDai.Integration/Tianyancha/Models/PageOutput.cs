namespace AhDai.Integration.Tianyancha.Models;

/// <summary>
/// PageOutput
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageOutput<T>
{
    /// <summary>
    /// 总数
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 结果
    /// </summary>
    public T[] Items { get; set; } = default!;
}
