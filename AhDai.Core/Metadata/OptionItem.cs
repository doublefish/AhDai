namespace AhDai.Core.Metadata;

/// <summary>
/// OptionItem
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="value"></param>
/// <param name="name"></param>
/// <param name="order"></param>
public class OptionItem<T>(T value, string name, int order = 0)
{
    /// <summary>
    /// 值
    /// </summary>
    public T Value { get; set; } = value;
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = name;
    /// <summary>
    /// 顺序
    /// </summary>
    public int Order { get; set; } = order;
}

