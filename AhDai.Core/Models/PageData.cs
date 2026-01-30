using System.Collections.Generic;

namespace AhDai.Core.Models;

/// <summary>
/// 分页数据
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="count"></param>
/// <param name="list"></param>
public class PageData<T>(int count, ICollection<T> list)
{
    /// <summary>
    /// 总数
    /// </summary>
    public int Count { get; set; } = count;
    /// <summary>
    /// 数据
    /// </summary>
    public ICollection<T> List { get; set; } = list;
}
