namespace AhDai.Integration.Hikvision.Models.IoT;

/// <summary>
/// 分页查询
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="data"></param>
/// <param name="count"></param>
public class PageOutput<T>(T[] data = default!, long count = 0)
{
    /// <summary>
    /// 数据
    /// </summary>
    public T[] Data { get; set; } = data;
    /// <summary>
    /// 总数
    /// </summary>
    public long Count { get; set; } = count;
}
