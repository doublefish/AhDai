namespace AhDai.Core.Models;

/// <summary>
/// DictItem
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TName"></typeparam>
/// <param name="value"></param>
/// <param name="name"></param>
/// <param name="sort"></param>
public class DictItem<TValue, TName>(TValue value, TName name, int sort = 0)
{
    /// <summary>
    /// 值
    /// </summary>
    public TValue Value { get; set; } = value;
    /// <summary>
    /// 名称
    /// </summary>
    public TName Name { get; set; } = name;
    /// <summary>
    /// 顺序
    /// </summary>
    public int Sort { get; set; } = sort;
}

/// <summary>
/// DictItem
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <param name="value"></param>
/// <param name="name"></param>
/// <param name="sort"></param>
public class DictItem<TValue>(TValue value, string name, int sort = 0)
    : DictItem<TValue, string>(value, name, sort)
{

}

